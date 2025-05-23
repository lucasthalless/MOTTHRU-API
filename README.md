# MOTTHRU-API

API RESTful desenvolvida em .NET 8 para gerenciamento de dados de motocicletas. Este projeto fornece operações CRUD (Create, Read, Update, Delete) para motos, com campos essenciais como placa, chassi e número do motor.

## 🚀 Tecnologias Utilizadas

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- ASP.NET Core Web API
- C#
- Docker
- Entity Framework Core
- Swagger

## ⚙️ Funcionalidades

- **Listar todas as motos**: Recupera todas as motos cadastradas.  
- **Obter moto por ID**: Recupera detalhes de uma moto específica pelo identificador.  
- **Cadastrar nova moto**: Adiciona uma nova moto ao sistema.  
- **Atualizar moto**: Altera dados de uma moto existente.  
- **Excluir moto**: Remove uma moto do sistema.  

## 📁 Estrutura do Projeto

```

MOTTHRU-API/
├── MOTTHRU.API/ ← Projeto Web API
│ ├── Controllers/ ← Controllers (endpoints)
│ ├── Application/
│ │ ├── Dtos/ ← Data Transfer Objects
│ │ └── Services/ ← Lógica de aplicação
│ ├── Domain/
│ │ └── Entities/ ← Entidades
│ └── Infrastructure/
│ │ └── Data/ ← DbContext e configurações de EF Core
└─└── Repositories/ ← Implementação de repositórios

```

## 🛠️ Instalação

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [Docker](https://www.docker.com/) (opcional)

### Clonando o Repositório

```bash
git clone https://github.com/lucasthalless/MOTTHRU-API.git
cd MOTTHRU-API
Executando a Aplicação
Via .NET CLI
bash
Copiar
Editar
dotnet restore
dotnet build
dotnet run --project MOTTHRU.API
A API ficará disponível em
https://localhost:5001
http://localhost:5000

Via Docker
bash
Copiar
Editar
docker build -t motthru-api .
docker run -d -p 5000:80 --name motthru-api motthru-api
A API ficará disponível em
http://localhost:5000

📄 Documentação da API
Após subir o serviço, abra no navegador:

bash
Copiar
Editar
http://localhost:5000/swagger
Lá você encontra todos os endpoints e pode testar as chamadas diretamente.

📌 Observações
Verifique se as portas 5000 e 5001 não estão em uso.

Ajuste appsettings.json para string de conexão com seu banco de dados.
