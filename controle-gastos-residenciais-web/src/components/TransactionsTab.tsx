import { useEffect, useMemo, useState, type JSX } from "react";
import TransactionService from "@/services/TransactionService";
import PersonService from "@/services/PersonService";
import CategoryService from "@/services/CategoryService";
import type { TransactionRequestDto } from "@/types/transaction/Transaction";
import type { PersonResponseDto } from "@/types/person/Person";
import type { CategoryResponseDto } from "@/types/category/Category";
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
import { Badge } from "@/components/ui/badge";
import { Plus } from "lucide-react";
import { toast } from "sonner";
import { formatCurrency } from "@/utils/calculations";
import type {
  TransactionResponseDto,
  TransactionType,
} from "@/types/transaction/Transaction";

type UiType = "despesa" | "receita";

export function TransactionsTab(): JSX.Element {
  const [transactions, setTransactions] = useState<TransactionResponseDto[]>(
    []
  );
  const [people, setPeople] = useState<PersonResponseDto[]>([]);
  const [categories, setCategories] = useState<CategoryResponseDto[]>([]);
  const [availableCategories, setAvailableCategories] = useState<
    CategoryResponseDto[]
  >([]);

  const [description, setDescription] = useState("");
  const [value, setValue] = useState("");
  const [uiType, setUiType] = useState<UiType>("despesa");
  const [categoryId, setCategoryId] = useState("");
  const [personId, setPersonId] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const loadData = async (): Promise<void> => {
      try {
        const [peopleData, categoryData, transactionData] = await Promise.all([
          PersonService.getAll(),
          CategoryService.getAll(),
          TransactionService.getAll(),
        ]);

        setPeople(peopleData);
        setCategories(categoryData);
        setTransactions(transactionData);
      } catch (error) {
        toast.error("Erro ao carregar dados");
        console.error(error);
      }
    };

    void loadData();
  }, []);

  useEffect(() => {
    const filtered = categories.filter((cat) => {
      if (uiType === "despesa") {
        return cat.purpose === 0 || cat.purpose === 2;
      }
      return cat.purpose === 1 || cat.purpose === 2;
    });

    setAvailableCategories(filtered);

    if (categoryId && !filtered.find((c) => c.id === categoryId)) {
      setCategoryId("");
    }
  }, [uiType, categories, categoryId]);

  const handleSubmit = async (e: React.FormEvent): Promise<void> => {
    e.preventDefault();

    const valueNum = parseFloat(value);
    if (isNaN(valueNum) || valueNum <= 0) {
      toast.error("Valor deve ser um número positivo");
      return;
    }

    const selectedPerson = people.find((p) => p.id === personId);
    if (!selectedPerson) {
      toast.error("Selecione uma pessoa");
      return;
    }

    // Regra de negócio antiga (se ainda fizer sentido):
    if (selectedPerson.age < 18 && uiType === "receita") {
      toast.error("Menores de 18 anos só podem ter despesas cadastradas");
      return;
    }

    if (!categoryId) {
      toast.error("Selecione uma categoria");
      return;
    }

    const apiType: TransactionType = uiType === "despesa" ? 0 : 1;

    const payload: TransactionRequestDto = {
      description: description.trim(),
      value: valueNum,
      type: apiType,
      personId,
      categoryId,
    };

    try {
      setLoading(true);
      await TransactionService.createTransaction(payload);
      toast.success("Transação cadastrada com sucesso");

      setDescription("");
      setValue("");
      setUiType("despesa");
      setCategoryId("");
      setPersonId("");

      // Recarrega apenas as transações
      const updated = await TransactionService.getAll();
      setTransactions(updated);
    } catch (error) {
      toast.error("Erro ao cadastrar transação");
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const getTypeBadgeVariant = (type: TransactionType) =>
    type === 0 ? "destructive" : "default";

  const getTypeLabel = (type: TransactionType): string =>
    type === 0 ? "Despesa" : "Receita";

  const categoryMap = useMemo(
    () =>
      categories.reduce<Record<string, string>>((acc, cat) => {
        acc[cat.id] = cat.description;
        return acc;
      }, {}),
    [categories]
  );

  const personMap = useMemo(
    () =>
      people.reduce<Record<string, string>>((acc, p) => {
        acc[p.id] = p.name;
        return acc;
      }, {}),
    [people]
  );

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Cadastrar Nova Transação</CardTitle>
          <CardDescription>
            Registre uma nova receita ou despesa
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
                  placeholder="Ex: Compra de supermercado"
                  required
                />
              </div>
              <div className="space-y-2 text-center">
                <Label htmlFor="value">Valor</Label>
                <Input
                  id="value"
                  type="number"
                  step="0.01"
                  min="0.01"
                  value={value}
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                    setValue(e.target.value)
                  }
                  placeholder="0.00"
                  required
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="type">Tipo</Label>
                <Select
                  value={uiType}
                  onValueChange={(value: UiType) => setUiType(value)}
                >
                  <SelectTrigger id="type">
                    <SelectValue />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="despesa">Despesa</SelectItem>
                    <SelectItem value="receita">Receita</SelectItem>
                  </SelectContent>
                </Select>
              </div>
              <div className="space-y-2">
                <Label htmlFor="category">Categoria</Label>
                <Select
                  value={categoryId}
                  onValueChange={(value: string) => setCategoryId(value)}
                >
                  <SelectTrigger id="category">
                    <SelectValue placeholder="Selecione uma categoria" />
                  </SelectTrigger>
                  <SelectContent>
                    {availableCategories.map((cat) => (
                      <SelectItem key={cat.id} value={cat.id}>
                        {cat.description}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
              <div className="space-y-2 md:col-span-2">
                <Label htmlFor="person">Pessoa</Label>
                <Select
                  value={personId}
                  onValueChange={(value: string) => setPersonId(value)}
                >
                  <SelectTrigger id="person">
                    <SelectValue placeholder="Selecione uma pessoa" />
                  </SelectTrigger>
                  <SelectContent>
                    {people.map((person) => (
                      <SelectItem key={person.id} value={person.id}>
                        {person.name} ({person.age} anos)
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
            </div>
            <Button type="submit" disabled={loading}>
              <Plus className="mr-2 h-4 w-4" />
              Cadastrar Transação
            </Button>
          </form>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Transações Cadastradas</CardTitle>
          <CardDescription>
            Lista de todas as transações no sistema
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead className="text-center">Descrição</TableHead>
                <TableHead className="text-center">Pessoa</TableHead>
                <TableHead className="text-center">Categoria</TableHead>
                <TableHead className="text-center">Tipo</TableHead>
                <TableHead className="text-center">Valor</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {transactions.length === 0 ? (
                <TableRow>
                  <TableCell
                    colSpan={5}
                    className="text-center text-muted-foreground"
                  >
                    Nenhuma transação cadastrada
                  </TableCell>
                </TableRow>
              ) : (
                transactions.map((t) => (
                  <TableRow key={t.id}>
                    <TableCell className="font-medium">
                      {t.description}
                    </TableCell>
                    <TableCell>
                      {t.personName ?? personMap[t.personId]}
                    </TableCell>
                    <TableCell>{categoryMap[t.categoryId]}</TableCell>
                    <TableCell>
                      <Badge variant={getTypeBadgeVariant(t.type)}>
                        {getTypeLabel(t.type)}
                      </Badge>
                    </TableCell>
                    <TableCell className="text-center font-medium">
                      {formatCurrency(t.value)}
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
