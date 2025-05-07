# Aplicação para o Desafio da DataSystem

Este projeto é uma aplicação fullstack que utiliza:

- **Backend:** ASP.NET Core Web API
- **Frontend:** React.js com Vite
- **Banco de Dados:** SQL Server
- **IDE:** Visual Studio 2022 Community Edition

A aplicação oferece uma API RESTful conectada a um banco de dados relacional e uma interface web interativa construída com React.js.

---

## Tecnologias Utilizadas

- ASP.NET Core 6.0 ou superior
- React.js 18+ com Vite
- SQL Server 2019+
- Entity Framework Core
- Visual Studio 2022 Community
- Node.js / npm / yarn


---

## Como Configurar o Projeto Localmente

### Pré-requisitos

- Visual Studio 2022 Community com a carga de trabalho:
  - **Desenvolvimento ASP.NET e web**
- Node.js 18+ [Instalar](https://nodejs.org/)
- .NET 6 SDK ou superior [Instalar](https://dotnet.microsoft.com/download)
- SQL Server (local)

---

## Passos para Configuração

### 1. Clonar o Repositório

bash
git clone git@github.com:angsant/DesafioDataSystem.git
cd DesafioDataSystem

### 2. Banco de dados

- String de conexão com banco de dados:

  - Data Source=DESKTOP-JVDLEU4;Initial Catalog=gerenciadortarefas;User ID=sa;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;

- Sistema de Gerenciamento: SQL Server

- ORM: Entity Framework Core

- Configuração: appsettings.Development.json

- Context já possue migration

- Comandos para migration:

  - add-migration Init -Context DesafioDataSystemDbContext

  - update-database

### 3. Inicializar aplicação no Visual Studio:

  - Abrir a solução DesafioDataSystem.API.sln
  
  - Nas configurações de projeto, configurar para executar os projetos frontend2 e DesafioDataSystem.API
  
  - API executada em https
  
  - Frontend executado no navegador Edge
  
  - Clicar na botão Run

### 4. Notas finais

- Habilite o CORS no backend (Program.cs) para permitir requisições do frontend

- Certifique-se de que Visual Studio e SQL Server estejam rodando como Administrador, se necessário

- A porta de execução do frontend é 59648

- A porta de execução da API é 7122

  
