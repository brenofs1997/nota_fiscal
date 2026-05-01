# Sistema de Gestão de Faturamento e Estoque (Korp Teste Técnico)

Este projeto consiste em um ecossistema de microsserviços para o gerenciamento de produtos, controle de estoque e emissão de notas fiscais. A solução foi desenvolvida com foco em escalabilidade, resiliência e boas práticas de arquitetura (Clean Code).

---

## 🛠️ Como Executar o Projeto

### 1. Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v18 ou superior)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)
- [Docker](https://www.docker.com/) (Opcional)

### 2. Execução via Docker (Recomendado)
Na raiz do projeto, onde se encontra os arquivos Db.ServicoEstoque e Db.ServicoFaturamento `docker-compose.yml`:
```bash
docker-compose up --build
Acesse o sistema em: http://localhost:4200

3. Execução Manual (Desenvolvimento)
Backend (Serviços)
Entre na pasta de cada serviço e execute:

Bash
# Serviço de Estoque
cd backend/ServicoEstoque
dotnet run

# Serviço de Faturamento
cd backend/ServicoFaturamento
dotnet run
Frontend (Angular)
Bash
cd frontend
npm install
ng serve
Acesse: http://localhost:4200