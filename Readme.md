# Sistema de Gerenciamento Financeiro

Sistema completo para controle de finanças pessoais, desenvolvido com **.NET Core** (backend) e **React** (frontend), seguindo os princípios de **Clean Architecture**.

---

## Visão Geral

O **Sistema de Gerenciamento Financeiro** permite:

- Cadastro de pessoas e categorias
- Registro de transações
- Relatórios consolidados por pessoa e categoria
- Validações de regras de negócio
- Interface moderna e responsiva

---

## Tecnologias Utilizadas

### Backend
- **.NET 9** 
- **Entity Framework Core**
- **SQLite**
- **FluentValidation**
- **Clean Architecture**

### Frontend
- **React** com **TypeScript**
- **Vite**
- **Axios**
- **shadcn/ui** 
- **Tailwind CSS** 
- **Sonner** 

---


Navegue até a pasta do projeto de API:

```bash
cd ControleGastosResidenciais
```

### Execute o backend

```bash
dotnet run
```

A API estará disponível em:
- **HTTPS:** `https://localhost:7068`
- **HTTP:** `http://localhost:5068`

Acesse o Swagger para testar os endpoints:
- `https://localhost:7068/swagger`

---

## Configuração do Frontend

### 1. Navegue até a pasta do frontend

```bash
cd controle-gastos-residenciais-web
```

### 2. Instale as dependências

```bash
npm install
```

ou

```bash
yarn install
```

### 3. Configure a URL da API

Crie um arquivo `.env` na raiz do projeto frontend:

```env
VITE_API_URL=https://localhost:7068
```

### 4. Execute o frontend

```bash
npm run dev
```

ou

```bash
yarn dev
```

O frontend estará disponível em:
- `http://localhost:5173`

---

## Executando a Aplicação

### Ordem de inicialização

1. **Inicie o backend primeiro:**
   ```bash
   dotnet run
   ```

2. **Em outro terminal, inicie o frontend:**
   ```bash
   npm run dev
   ```

3. **Acesse a aplicação:**
   - Frontend: `http://localhost:5173`
   - Backend (Swagger): `https://localhost:7068/swagger`

---

## Funcionalidades

### 1. Gestão de Pessoas
-  Cadastrar pessoa (nome e idade)
-  Listar todas as pessoas
-  Deletar pessoa (remove transações relacionadas em cascata)

### 2. Gestão de Categorias
-  Cadastrar categoria (descrição e finalidade: Receita, Despesa ou Ambas)
-  Listar todas as categorias
-  Deletar categoria

### 3. Gestão de Transações
-  Cadastrar transação (descrição, valor, tipo, pessoa, categoria)
-  Listar todas as transações
-  Validação: menores de 18 anos só podem ter despesas
-  Filtro dinâmico de categorias por tipo de transação

### 4. Relatórios
- Relatório por pessoa (total de receitas, despesas e saldo)
- Relatório por categoria (total de receitas, despesas e saldo)
- Filtros dinâmicos (por pessoa específica ou todas)

---