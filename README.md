# MOTTHRU API

## 📋 Integrantes

* [Lucas Thalles dos Santos](https://github.com/lucasthalless)
* [Carolina Estevam Rodgerio](https://github.com/carolrodgerio)
* [Enrico Andrade D'Amico](https://github.com/enrico-ad)

---

## 🏗️ Justificativa da Arquitetura

O projeto foi desenvolvido seguindo **Clean Architecture** e **DDD**, garantindo:

* Separação clara entre **Domain**, **Application**, **Infrastructure** e **Presentation**.
* Facilitação de testes unitários e integração.
* Código modular, escalável e fácil de manter.
* Documentação integrada com **Swagger** e exemplos de request/response.
* Pontos para evolução: modularização em soluções.

As entidades implementadas foram: Moto, Patio e Rfid. Atualmente, toda moto vai estar relacionada com um pátio e armazenaremos também a informação de RFID de cada moto. Embora já  existam novas entidades a serem implementadas, com essas informações, será possível iniciar o desenvolvimento da proposta **MOTTHRU**.

---

## 🚀 Instruções de Execução

1. Clone o repositório:

```bash
git clone https://github.com/lucasthalless/MOTTHRU-API.git
```

2. Entre na pasta do projeto:

```bash
cd MOTTHRU.API
```

3. Rode o Docker compose para executar o banco Postgres:

```bash
docker-compose up
```

Caso esteja usando outro banco, configure a **connection string** no `appsettings.json` conforme necessário:

```json
{
  "ConnectionStrings": {
    "Postgres": "Host=localhost;Port=5433;Database=motthru_postgres;Username=admin;Password=secret"
  }
}
```

4. Restaure os pacotes NuGet:

```bash
dotnet restore
```

5. Execute as migrations e atualize o banco de dados:

```bash
dotnet ef database update --project MOTTHRU.API --startup-project MOTTHRU.API
```

6. Execute a API:

```bash
dotnet run --project MOTTHRU.API
```

7. Acesse a documentação Swagger para testar os endpoints:

```
http://localhost:5022/swagger
```

---

## 📌 Exemplos de Uso dos Endpoints com HATEOAS

### **MOTO**

* **GET** `/api/moto`

```json
{
  "data": [
    {
      "id": 100,
      "placa": "AAA1A11",
      "chassi": "CHASSI1234567890",
      "numMotor": "MOTOR123",
      "patioId": 1,
      "links": {
        "self": "/api/moto/100",
        "put": "/api/moto/100",
        "delete": "/api/moto/100"
      }
    }
  ],
  "links": {
    "self": "/api/moto",
    "create": "/api/moto"
  },
  "pagina": {
    "Deslocamento": 0,
    "RegistrosRetornado": 10,
    "TotalRegistros": 1
  }
}
```

* **GET** `/api/moto/{id}`

```json
{
  "data": {
    "id": 100,
    "placa": "AAA1A11",
    "chassi": "CHASSI1234567890",
    "numMotor": "MOTOR123",
    "patioId": 1
  },
  "links": {
    "self": "/api/moto/100",
    "get": "/api/moto",
    "put": "/api/moto/100",
    "delete": "/api/moto/100"
  }
}
```

* **POST** `/api/moto`

```json
{
  "placa": "AAA1A11",
  "chassi": "CHASSI1234567890",
  "numMotor": "MOTOR123",
  "patioId": 1
}
```

* **PUT** `/api/moto/{id}`

```json
{
  "placa": "AAA1A11",
  "chassi": "CHASSI1234567890",
  "numMotor": "MOTOR999",
  "patioId": 2
}
```

* **DELETE** `/api/moto/{id}`

---

### **PATIO**

* **GET** `/api/patio`

```json
{
  "data": [
    {
      "id": 1,
      "nomePatio": "Patio Central",
      "links": {
        "self": "/api/patio/1",
        "put": "/api/patio/1",
        "delete": "/api/patio/1"
      }
    }
  ],
  "links": {
    "self": "/api/patio",
    "create": "/api/patio"
  }
}
```

* **POST** `/api/patio`

```json
{
  "nomePatio": "Patio Central"
}
```

* **PUT** `/api/patio/{id}`

```json
{
  "nomePatio": "Patio Sul"
}
```

* **DELETE** `/api/patio/{id}`

---

### **RFID**

* **GET** `/api/rfid`

```json
{
  "data": [
    {
      "id": 200,
      "sinal": "ABC123XYZ",
      "motoId": 100,
      "links": {
        "self": "/api/rfid/200",
        "put": "/api/rfid/200",
        "delete": "/api/rfid/200"
      }
    }
  ],
  "links": {
    "self": "/api/rfid",
    "create": "/api/rfid"
  }
}
```

* **POST** `/api/rfid`

```json
{
  "sinal": "ABC123XYZ",
  "motoId": 100
}
```

* **PUT** `/api/rfid/{id}`

```json
{
  "sinal": "XYZ789ABC",
  "motoId": 100
}
```

* **DELETE** `/api/rfid/{id}`

---

## 🧪 Rodando os Testes

Execute todos os testes do projeto com o comando:

```bash
dotnet test
```

Isso executará os **unit tests** de UseCases e Repositories.
