import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { PersonsTab } from "@/components/PersonsTab";
import { CategoriesTab } from "@/components/CategoriesTab";
import { TransactionsTab } from "@/components/TransactionsTab";
import { SummariesTab } from "@/components/SummariesTab";
import { Toaster } from "sonner";
import { Wallet } from "lucide-react";
import "./App.css";

function App() {
  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 to-slate-100 dark:from-slate-950 dark:to-slate-900">
      <div className="container mx-auto py-8 px-4">
        <div className="mb-8">
          <div className="flex items-center gap-3 mb-2">
            <div className="p-2 bg-slate-900 dark:bg-slate-100 rounded-lg">
              <Wallet className="h-6 w-6 text-slate-50 dark:text-slate-900" />
            </div>
            <h1 className="text-3xl font-bold tracking-tight">
              Sistema de Gerenciamento Financeiro
            </h1>
          </div>
          <p className="text-muted-foreground">
            Gerencie suas finanças de forma simples e eficiente
          </p>
        </div>

        <Tabs defaultValue="persons" className="w-full">
          <TabsList className="grid w-full grid-cols-5 mb-8">
            <TabsTrigger value="persons">Pessoas</TabsTrigger>
            <TabsTrigger value="categories">Categorias</TabsTrigger>
            <TabsTrigger value="transactions">Transações</TabsTrigger>
            <TabsTrigger value="summaries">Relatórios</TabsTrigger>
          </TabsList>

          <TabsContent value="persons">
            <PersonsTab />
          </TabsContent>

          <TabsContent value="categories">
            <CategoriesTab />
          </TabsContent>

          <TabsContent value="transactions">
            <TransactionsTab />
          </TabsContent>

          <TabsContent value="summaries">
            <SummariesTab />
          </TabsContent>
        </Tabs>
      </div>
      <Toaster richColors position="top-right" />
    </div>
  );
}

export default App;
