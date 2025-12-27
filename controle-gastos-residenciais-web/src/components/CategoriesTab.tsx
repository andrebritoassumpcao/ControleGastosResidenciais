import { useState, useEffect } from "react";
import CategoryService from "@/services/CategoryService";
import type {
  CategoryRequestDto,
  CategoryResponseDto,
  CategoryPurpose,
} from "@/types/category/Category";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";
import { Badge } from "@/components/ui/badge";
import { Plus, Trash2 } from "lucide-react";
import { toast } from "sonner";

export function CategoriesTab() {
  const [categories, setCategories] = useState<CategoryResponseDto[]>([]);
  const [description, setDescription] = useState("");
  const [purpose, setPurpose] = useState<CategoryPurpose>(2);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    void loadCategories();
  }, []);

  const loadCategories = async (): Promise<void> => {
    try {
      const data = await CategoryService.getAll();
      setCategories(data);
    } catch (error) {
      toast.error("Erro ao carregar categorias");
      console.error(error);
    }
  };

  const handleSubmit = async (e: React.FormEvent): Promise<void> => {
    e.preventDefault();

    const categoryData: CategoryRequestDto = {
      description: description.trim(),
      purpose,
    };

    setLoading(true);
    try {
      await CategoryService.createCategory(categoryData);
      toast.success("Categoria cadastrada com sucesso");
      setDescription("");
      setPurpose(2);
      await loadCategories();
    } catch (error) {
      toast.error("Erro ao cadastrar categoria");
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (
    id: string,
    description: string
  ): Promise<void> => {
    try {
      await CategoryService.deleteCategory(id);
      toast.success(`${description} foi removida com sucesso`);
      await loadCategories();
    } catch (error) {
      toast.error("Erro ao deletar categoria");
      console.error(error);
    }
  };

  const getBadgeVariant = (p: CategoryPurpose) => {
    switch (p) {
      case 1:
        return "default";
      case 0:
        return "destructive";
      case 2:
      default:
        return "secondary";
    }
  };

  const getPurposeLabel = (p: CategoryPurpose): string => {
    switch (p) {
      case 1:
        return "Receita";
      case 0:
        return "Despesa";
      case 2:
        return "Ambas";
      default:
        return String(p);
    }
  };

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Cadastrar Nova Categoria</CardTitle>
          <CardDescription>
            Adicione uma nova categoria de transação
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit} className="space-y-4">
            <div className="grid gap-4 md:grid-cols-2">
              <div className="space-y-2">
                <Label htmlFor="description">Descrição</Label>
                <Input
                  id="description"
                  value={description}
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                    setDescription(e.target.value)
                  }
                  placeholder="Ex: Alimentação, Salário, etc."
                  required
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="purpose">Finalidade</Label>
                <Select
                  value={String(purpose)}
                  onValueChange={(value: string) =>
                    setPurpose(Number(value) as CategoryPurpose)
                  }
                >
                  <SelectTrigger id="purpose">
                    <SelectValue />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="EXPENSE">Despesa</SelectItem>
                    <SelectItem value="INCOME">Receita</SelectItem>
                    <SelectItem value="BOTH">Ambas</SelectItem>
                  </SelectContent>
                </Select>
              </div>
            </div>
            <Button type="submit" disabled={loading}>
              <Plus className="mr-2 h-4 w-4" />
              Cadastrar Categoria
            </Button>
          </form>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Categorias Cadastradas</CardTitle>
          <CardDescription>
            Lista de todas as categorias no sistema
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Descrição</TableHead>
                <TableHead>Finalidade</TableHead>
                <TableHead className="w-[100px]">Ações</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {categories.length === 0 ? (
                <TableRow>
                  <TableCell
                    colSpan={3}
                    className="text-center text-muted-foreground"
                  >
                    Nenhuma categoria cadastrada
                  </TableCell>
                </TableRow>
              ) : (
                categories.map((category) => (
                  <TableRow key={category.id}>
                    <TableCell className="font-medium">
                      {category.description}
                    </TableCell>
                    <TableCell>
                      <Badge variant={getBadgeVariant(category.purpose)}>
                        {getPurposeLabel(category.purpose)}
                      </Badge>
                    </TableCell>
                    <TableCell>
                      <AlertDialog>
                        <AlertDialogTrigger asChild>
                          <Button variant="ghost" size="icon">
                            <Trash2 className="h-4 w-4 text-red-600" />
                          </Button>
                        </AlertDialogTrigger>
                        <AlertDialogContent>
                          <AlertDialogHeader>
                            <AlertDialogTitle>
                              Confirmar exclusão
                            </AlertDialogTitle>
                            <AlertDialogDescription>
                              Tem certeza que deseja excluir{" "}
                              {category.description}? Esta ação não pode ser
                              desfeita e todas as transações desta categoria
                              serão removidas.
                            </AlertDialogDescription>
                          </AlertDialogHeader>
                          <AlertDialogFooter>
                            <AlertDialogCancel>Cancelar</AlertDialogCancel>
                            <AlertDialogAction
                              onClick={() =>
                                void handleDelete(
                                  category.id,
                                  category.description
                                )
                              }
                            >
                              Excluir
                            </AlertDialogAction>
                          </AlertDialogFooter>
                        </AlertDialogContent>
                      </AlertDialog>
                    </TableCell>
                  </TableRow>
                ))
              )}
            </TableBody>
          </Table>
        </CardContent>
      </Card>
    </div>
  );
}
