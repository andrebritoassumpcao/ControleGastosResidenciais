import { useEffect, useState, type JSX } from "react";
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
  TableFooter,
} from "@/components/ui/table";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Badge } from "@/components/ui/badge";
import { TrendingUp, TrendingDown, DollarSign } from "lucide-react";
import { toast } from "sonner";
import { formatCurrency } from "@/utils/calculations";
import SummaryService from "@/services/SummaryService";
import TransactionService from "@/services/TransactionService";
import type {
  SummaryByPersonResponseDto,
  SummaryByCategoryResponseDto,
  SummaryByPersonDto,
  SummaryByCategoryDto,
} from "@/types/summary/Summary";
import type { TransactionResponseDto } from "@/types/transaction/Transaction";

type ReportType = "person" | "category";

export function SummariesTab(): JSX.Element {
  const [reportType, setReportType] = useState<ReportType>("person");
  const [loading, setLoading] = useState<boolean>(true);

  const [personSummary, setPersonSummary] =
    useState<SummaryByPersonResponseDto | null>(null);
  const [categorySummary, setCategorySummary] =
    useState<SummaryByCategoryResponseDto | null>(null);

  const [selectedPersonSummary, setSelectedPersonSummary] =
    useState<SummaryByPersonDto | null>(null);
  const [selectedCategorySummary, setSelectedCategorySummary] =
    useState<SummaryByCategoryDto | null>(null);

  const [selectedId, setSelectedId] = useState<string>("");
  const [filteredTransactions, setFilteredTransactions] = useState<
    TransactionResponseDto[]
  >([]);
  const [loadingTransactions, setLoadingTransactions] =
    useState<boolean>(false);

  useEffect(() => {
    const loadData = async (): Promise<void> => {
      try {
        setLoading(true);
        const [personData, categoryData] = await Promise.all([
          SummaryService.getAllPersonSummary(),
          SummaryService.getAllCategorySummary(),
        ]);
        setPersonSummary(personData);
        setCategorySummary(categoryData);
      } catch (error) {
        toast.error("Erro ao carregar relatórios");
        console.error(error);
      } finally {
        setLoading(false);
      }
    };

    void loadData();
  }, []);

  useEffect(() => {
    setSelectedId("");
    setFilteredTransactions([]);
    setSelectedPersonSummary(null);
    setSelectedCategorySummary(null);
  }, [reportType]);

  useEffect(() => {
    if (!selectedId) {
      setFilteredTransactions([]);
      setSelectedPersonSummary(null);
      setSelectedCategorySummary(null);
      return;
    }

    const loadSelectedData = async (): Promise<void> => {
      try {
        setLoadingTransactions(true);

        const allTransactions = await TransactionService.getAll();

        const filtered = allTransactions.filter((t) =>
          reportType === "person"
            ? t.personId === selectedId
            : t.categoryId === selectedId
        );

        setFilteredTransactions(filtered);

        // Carrega resumo específico (item único)
        if (reportType === "person") {
          const specificSummary = await SummaryService.getPersonSummaryById(
            selectedId
          );
          setSelectedPersonSummary(specificSummary);
        } else {
          const specificSummary = await SummaryService.getCategorySummaryById(
            selectedId
          );
          setSelectedCategorySummary(specificSummary);
        }
      } catch (error) {
        toast.error("Erro ao carregar dados filtrados");
        console.error(error);
      } finally {
        setLoadingTransactions(false);
      }
    };

    void loadSelectedData();
  }, [selectedId, reportType]);

  if (loading) {
    return (
      <Card>
        <CardContent className="pt-6">
          <p className="text-center text-muted-foreground">Carregando...</p>
        </CardContent>
      </Card>
    );
  }

  const currentSummary =
    reportType === "person" ? personSummary : categorySummary;

  if (!currentSummary || currentSummary.items.length === 0) {
    return (
      <Card>
        <CardHeader>
          <CardTitle>Relatórios</CardTitle>
          <CardDescription>Nenhum dado disponível</CardDescription>
        </CardHeader>
      </Card>
    );
  }

  const totaisGerais = (() => {
    if (reportType === "person" && selectedPersonSummary) {
      return {
        receitas: selectedPersonSummary.totalIncome,
        despesas: selectedPersonSummary.totalExpenses,
        saldo: selectedPersonSummary.balance,
      };
    }

    if (reportType === "category" && selectedCategorySummary) {
      return {
        receitas: selectedCategorySummary.totalIncome,
        despesas: selectedCategorySummary.totalExpenses,
        saldo: selectedCategorySummary.balance,
      };
    }

    return {
      receitas: currentSummary.totalIncome,
      despesas: currentSummary.totalExpense,
      saldo: currentSummary.balance,
    };
  })();

  const items = currentSummary.items as Array<
    SummaryByPersonDto | SummaryByCategoryDto
  >;

  const getItemId = (item: SummaryByPersonDto | SummaryByCategoryDto): string =>
    "personId" in item ? item.personId : item.categoryId;

  const getItemName = (
    item: SummaryByPersonDto | SummaryByCategoryDto
  ): string => ("name" in item ? item.name : item.description);

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Tipo de Relatório</CardTitle>
          <CardDescription>
            Escolha entre relatório por pessoa ou por categoria
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Select
            value={reportType}
            onValueChange={(value: ReportType) => setReportType(value)}
          >
            <SelectTrigger>
              <SelectValue />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="person">Por Pessoa</SelectItem>
              <SelectItem value="category">Por Categoria</SelectItem>
            </SelectContent>
          </Select>
        </CardContent>
      </Card>

      <div className="grid gap-4 md:grid-cols-3">
        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">
              Total Receitas
            </CardTitle>
            <TrendingUp className="h-4 w-4 text-green-600" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold text-green-600">
              {formatCurrency(totaisGerais.receitas)}
            </div>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">
              Total Despesas
            </CardTitle>
            <TrendingDown className="h-4 w-4 text-red-600" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold text-red-600">
              {formatCurrency(totaisGerais.despesas)}
            </div>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Saldo Líquido</CardTitle>
            <DollarSign className="h-4 w-4" />
          </CardHeader>
          <CardContent>
            <div
              className={`text-2xl font-bold ${
                totaisGerais.saldo >= 0 ? "text-green-600" : "text-red-600"
              }`}
            >
              {formatCurrency(totaisGerais.saldo)}
            </div>
          </CardContent>
        </Card>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>
            {reportType === "person"
              ? "Totais por Pessoa"
              : "Totais por Categoria"}
          </CardTitle>
          <CardDescription>
            Relatório financeiro de cada{" "}
            {reportType === "person" ? "pessoa" : "categoria"} cadastrada
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead className="text-center">
                  {reportType === "person" ? "Pessoa" : "Categoria"}
                </TableHead>
                <TableHead className="text-center">Receitas</TableHead>
                <TableHead className="text-center">Despesas</TableHead>
                <TableHead className="text-center">Saldo</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {items.map((item) => {
                const itemId = getItemId(item);
                const itemName = getItemName(item);
                const isSelected = selectedId === itemId;

                return (
                  <TableRow
                    key={itemId}
                    className={`cursor-pointer transition-colors ${
                      isSelected ? "bg-muted" : "hover:bg-muted/50"
                    }`}
                    onClick={() => setSelectedId(isSelected ? "" : itemId)}
                  >
                    <TableCell className="font-medium text-center">
                      {itemName}
                      {isSelected && (
                        <Badge variant="outline" className="ml-2">
                          Selecionado
                        </Badge>
                      )}
                    </TableCell>
                    <TableCell className="text-center text-green-600">
                      {formatCurrency(item.totalIncome)}
                    </TableCell>
                    <TableCell className="text-center text-red-600">
                      {formatCurrency(item.totalExpenses)}
                    </TableCell>
                    <TableCell
                      className={`text-center font-medium ${
                        item.balance >= 0 ? "text-green-600" : "text-red-600"
                      }`}
                    >
                      {formatCurrency(item.balance)}
                    </TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
            <TableFooter>
              <TableRow>
                <TableCell className="font-bold">Total Geral</TableCell>
                <TableCell className="text-center font-bold text-green-600">
                  {formatCurrency(totaisGerais.receitas)}
                </TableCell>
                <TableCell className="text-center font-bold text-red-600">
                  {formatCurrency(totaisGerais.despesas)}
                </TableCell>
                <TableCell
                  className={`text-center font-bold ${
                    totaisGerais.saldo >= 0 ? "text-green-600" : "text-red-600"
                  }`}
                >
                  {formatCurrency(totaisGerais.saldo)}
                </TableCell>
              </TableRow>
            </TableFooter>
          </Table>
        </CardContent>
      </Card>

      {selectedId && (
        <Card>
          <CardHeader>
            <CardTitle>Transações Filtradas</CardTitle>
            <CardDescription>
              Transações da {reportType === "person" ? "pessoa" : "categoria"}{" "}
              selecionada
            </CardDescription>
          </CardHeader>
          <CardContent>
            {loadingTransactions ? (
              <p className="text-center text-muted-foreground">
                Carregando transações...
              </p>
            ) : filteredTransactions.length === 0 ? (
              <p className="text-center text-muted-foreground">
                Nenhuma transação encontrada
              </p>
            ) : (
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead className="text-center">Descrição</TableHead>
                    <TableHead className="text-center">
                      {reportType === "person" ? "Categoria" : "Pessoa"}
                    </TableHead>
                    <TableHead className="text-center">Tipo</TableHead>
                    <TableHead className="text-center">Valor</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {filteredTransactions.map((t) => (
                    <TableRow key={t.id}>
                      <TableCell className="font-medium text-center">
                        {t.description}
                      </TableCell>
                      <TableCell className="text-center">
                        {reportType === "person"
                          ? categorySummary?.items.find(
                              (c) => c.categoryId === t.categoryId
                            )?.description || "—"
                          : t.personName || "—"}
                      </TableCell>
                      <TableCell className="text-center">
                        <Badge
                          variant={t.type === 0 ? "destructive" : "default"}
                        >
                          {t.type === 0 ? "Despesa" : "Receita"}
                        </Badge>
                      </TableCell>
                      <TableCell className="text-center font-medium">
                        {formatCurrency(t.value)}
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            )}
          </CardContent>
        </Card>
      )}
    </div>
  );
}
