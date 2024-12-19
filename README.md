# Introdução 
E-Bookshop é um sistema desenvolvido para o gerenciamento de uma livraria. Este sistema permite o cadastro, consulta, edição e exclusão de livros, além de registrar vendas. A livraria pode usar este sistema para controlar os estoques, registrar vendas realizadas e visualizar relatórios básicos.

# Requisitos Funcionais

1. **Cadastro de Livros**
   - Cada livro deve conter as seguintes informações:
     - ISBN (código único)
     - Título
     - Autor(es)
     - Categoria (Ex.: Ficção, Não-ficção, Técnico, etc.)
     - Preço
     - Quantidade em estoque
   - Deve ser possível adicionar, editar e excluir livros.

2. **Registro de Vendas**
   - Deve ser possível registrar uma venda informando:
     - Livro(s) vendidos (um ou mais itens por venda)
     - Quantidade de cada item
     - Data da venda
   - A quantidade em estoque de cada livro deve ser ajustada automaticamente após a venda.

3. **Relatórios**
   - Relatório de livros com estoque abaixo de um limite configurável (ex.: menos de 5 unidades).
   - Relatório de vendas realizadas em um período especificado.

# Getting Started
Para rodar o código localmente, siga os seguintes passos:

1. **Processo de Instalação**
   - Clone o repositório: `git clone https://github.com/yourusername/e-bookshop.git`
   - Navegue até o diretório do projeto: `cd e-bookshop`

2. **Dependências de Software**
   - .NET SDK 8.0
   - PostgreSQL para persistência de dados

3. **Configure o banco de dados**
    - Vá até o arquivo [appsettings.json](backend-api/appsettings.json) e altere a string e conexão do banco de acordo com as configurações do seu banco de dados local.

4. **Referências da API**
   - A documentação da API está disponível no endpoint `/api/docs`.


# Executando os Projetos Localmente

1. **Backend (API)**
   - Navegue até o diretório do projeto backend: `cd backend-api`
   - Configure a string de conexão do PostgreSQL no arquivo `appsettings.json`.
   - Execute o projeto: `dotnet run`

   ```bash
   cd backend-api
   dotnet run
   ```

2. **Frontend (Razor Pages)**
   - Navegue até o diretório do projeto frontend: `cd frontend`
   - Execute o projeto: `dotnet run`

   ```bash
   cd frontend
   dotnet run
   ```