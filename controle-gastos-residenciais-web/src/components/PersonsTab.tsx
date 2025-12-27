import { useState, useEffect } from "react";
import PersonService from "@/services/PersonService";
import type {
  PersonRequestDto,
  PersonResponseDto,
} from "@/types/person/Person";
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
import { Plus, Trash2 } from "lucide-react";
import { toast } from "sonner";

export function PersonsTab() {
  const [people, setPeople] = useState<PersonResponseDto[]>([]);
  const [name, setName] = useState("");
  const [age, setAge] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    void loadPeople();
  }, []);

  const loadPeople = async (): Promise<void> => {
    try {
      const data = await PersonService.getAll();
      setPeople(data);
    } catch (error) {
      toast.error("Erro ao carregar pessoas");
      console.error(error);
    }
  };

  const handleSubmit = async (e: React.FormEvent): Promise<void> => {
    e.preventDefault();

    const ageNum = parseInt(age, 10);
    if (isNaN(ageNum) || ageNum <= 0) {
      toast.error("Idade deve ser um valor positivo");
      return;
    }

    const personData: PersonRequestDto = {
      name: name.trim(),
      age: ageNum,
    };

    setLoading(true);
    try {
      await PersonService.createPerson(personData);
      toast.success("Pessoa cadastrada com sucesso");
      setName("");
      setAge("");
      await loadPeople();
    } catch (error) {
      toast.error("Erro ao cadastrar pessoa");
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: string, name: string): Promise<void> => {
    try {
      await PersonService.deletePerson(id);
      toast.success(`${name} foi removido(a) com sucesso`);
      await loadPeople();
    } catch (error) {
      toast.error("Erro ao deletar pessoa");
      console.error(error);
    }
  };

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Cadastrar Nova Pessoa</CardTitle>
          <CardDescription>Adicione uma nova pessoa ao sistema</CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit} className="space-y-4">
            <div className="grid gap-4 md:grid-cols-2">
              <div className="space-y-2">
                <Label htmlFor="name">Nome</Label>
                <Input
                  id="name"
                  value={name}
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                    setName(e.target.value)
                  }
                  placeholder="Digite o nome"
                  required
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="age">Idade</Label>
                <Input
                  id="age"
                  type="number"
                  value={age}
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                    setAge(e.target.value)
                  }
                  placeholder="Digite a idade"
                  min="1"
                  required
                />
              </div>
            </div>
            <Button type="submit" disabled={loading}>
              <Plus className="mr-2 h-4 w-4" />
              Cadastrar Pessoa
            </Button>
          </form>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Pessoas Cadastradas</CardTitle>
          <CardDescription>
            Lista de todas as pessoas no sistema
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Nome</TableHead>
                <TableHead>Idade</TableHead>
                <TableHead className="w-[100px]">Ações</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {people.length === 0 ? (
                <TableRow>
                  <TableCell
                    colSpan={3}
                    className="text-center text-muted-foreground"
                  >
                    Nenhuma pessoa cadastrada
                  </TableCell>
                </TableRow>
              ) : (
                people.map((person) => (
                  <TableRow key={person.id}>
                    <TableCell className="font-medium">{person.name}</TableCell>
                    <TableCell>{person.age} anos</TableCell>
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
                              Tem certeza que deseja excluir {person.name}? Esta
                              ação não pode ser desfeita e todas as transações
                              desta pessoa serão removidas.
                            </AlertDialogDescription>
                          </AlertDialogHeader>
                          <AlertDialogFooter>
                            <AlertDialogCancel>Cancelar</AlertDialogCancel>
                            <AlertDialogAction
                              onClick={() =>
                                void handleDelete(person.id, person.name)
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
