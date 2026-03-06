# Financial Control System 💰

Sistema de controle de gastos residenciais desenvolvido com **.NET 9** (Clean Architecture & DDD) no Backend e **React + TypeScript + TailwindCSS** no Frontend. O banco de dados utilizado é o SQL Server, inicializado de forma simplificada utilizando Docker.

## 🚀 Tecnologias e Padrões Aplicados

- **Backend:** .NET 9, EF Core, Padrão Repository, Injeção de Dependências, CQRS (Simplificado), FluentValidation, xUnit, FluentAssertions, Swagger.
- **Frontend:** React, TypeScript, Vite, Tailwind CSS, Axios, React Router Dom, Lucide-React.
- **Infraestrutura:** Banco de Dados MS SQL Server via Docker Compose.

### 🌟 Destaques da Implementação (Regras de Negócio)
O sistema blinda e respeita todas as regras exigidas no teste tanto na API quanto no Frontend:

1. **Proteção para Menores de Idade**: Caso o usuário tente cadastrar uma transação para uma pessoa menor de 18 anos, o sistema (tanto Front quanto Back) trava a opção e aceita **apenas Despesas**.
2. **Filtro de Finalidade de Categoria**: Transações do tipo *Receita* não aceitam categorias exclusivas de *Despesa* (e vice-versa). O frontend oculta categorias incompatíveis dinamicamente.
3. **Exclusão em Cascata**: Ao excluir uma Pessoa, todas as suas transações são removidas automaticamente pelo banco de dados (configurado no EF Core via `OnDelete(DeleteBehavior.Cascade)`).
4. **Layout Responsivo e Relatórios em Tabela**: A listagem de totais consolidada por pessoa/categoria foi implementada em tabelas com rolagem horizontal (Mobile-Friendly).
5. **Autoproteção de CORS**: A API restringe requisições para a porta do Frontend (via appsettings.json).

---

## ⚙️ Pré-requisitos

Certifique-se de ter instalado em sua máquina:
- [Node.js](https://nodejs.org/) (Versão 18+ recomendada)
- [.NET SDK 9](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0)
- [Docker](https://www.docker.com/) e [Docker Compose](https://docs.docker.com/compose/)

---

## 🐳 Subindo o Banco de Dados (Docker)

O projeto utiliza um banco de dados MS SQL Server rodando num contêiner Docker na porta `1434`.

1. Navegue até o diretório do backend:
   ```bash
   cd backend
   ```
2. Inicialize o contêiner do SQL Server em segundo plano:
   ```bash
   docker-compose up -d
   ```
   *(A primeira vez pode demorar alguns minutos para baixar a imagem `mcr.microsoft.com/mssql/server:2022-latest`).*

---

## 🛠️ Executando o Backend (.NET 9)

O Backend roda na porta **7032** (HTTPS) ou **5031** (HTTP).

1. Navegue até seu diretório da API:
   ```bash
   cd backend/FinancialControl.API
   ```
2. Aplique as _migrations_ para criar as tabelas no banco de dados Docker:
   ```bash
   dotnet ef database update
   ```
3. Execute o projeto API:
   ```bash
   dotnet run
   ```
4. Verifique a documentação Swagger informada no console ou acesse:
   - [https://localhost:7032/swagger](https://localhost:7032/swagger)

---

## 💻 Executando o Frontend (React)

O Frontend foi desenvolvido utilizando o pacote ultrarrápido Vite e está configurado para consumir a API na porta `7032`.

1. Navegue até o diretório do frontend:
   ```bash
   cd frontend
   ```
2. Instale as dependências:
   ```bash
   npm install
   ```
3. Inicialize o servidor de desenvolvimento:
   ```bash
   npm run dev
   ```
4. O servidor irá expor a aplicação web no endereço:
   - [http://localhost:5173](http://localhost:5173)

---

## 🧪 Rodando os Testes Unitários

O projeto back-end tem cobertura robusta de testes unitários para o núcleo de domínio (`Domain Layer`), garantindo a consistência das entidades, Value Objects e regras restritas (como a do menor de idade).

1. Dentro da pasta inicial do backend (`cd backend`), execute:
   ```bash
   dotnet test
   ```
