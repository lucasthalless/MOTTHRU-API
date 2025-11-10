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

8. Execute todos **testes unitários** de Entidades, UseCases, Repositories e **testes de integração simples** do projeto com o comando:

```bash
dotnet test
```


---

## 📌 Exemplos de Uso dos Endpoints com HATEOAS

### **MOTO**

* **GET** `/api/v1/moto`

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
        "self": "/api/v1/moto/100",
        "put": "/api/v1/moto/100",
        "delete": "/api/v1/moto/100"
      }
    }
  ],
  "links": {
    "self": "/api/v1/moto",
    "create": "/api/v1/moto"
  },
  "pagina": {
    "Deslocamento": 0,
    "RegistrosRetornado": 10,
    "TotalRegistros": 1
  }
}
```

* **GET** `/api/v1/moto/{id}`

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
    "self": "/api/v1/moto/100",
    "get": "/api/v1/moto",
    "put": "/api/v1/moto/100",
    "delete": "/api/v1/moto/100"
  }
}
```

* **POST** `/api/v1/moto`

```json
{
  "placa": "AAA1A11",
  "chassi": "CHASSI1234567890",
  "numMotor": "MOTOR123",
  "patioId": 1
}
```

* **PUT** `/api/v1/moto/{id}`

```json
{
  "placa": "AAA1A11",
  "chassi": "CHASSI1234567890",
  "numMotor": "MOTOR999",
  "patioId": 2
}
```

* **DELETE** `/api/v1/moto/{id}`

---

### **PATIO**

* **GET** `/api/v1/patio`

```json
{
  "data": [
    {
      "id": 1,
      "nomePatio": "Patio Central",
      "links": {
        "self": "/api/v1/patio/1",
        "put": "/api/v1/patio/1",
        "delete": "/api/v1/patio/1"
      }
    }
  ],
  "links": {
    "self": "/api/v1/patio",
    "create": "/api/v1/patio"
  }
}
```

* **POST** `/api/v1/patio`

```json
{
  "nomePatio": "Patio Central"
}
```

* **PUT** `/api/v1/patio/{id}`

```json
{
  "nomePatio": "Patio Sul"
}
```

* **DELETE** `/api/v1/patio/{id}`

---

### **RFID**

* **GET** `/api/v1/rfid`

```json
{
  "data": [
    {
      "id": 200,
      "sinal": "ABC123XYZ",
      "motoId": 100,
      "links": {
        "self": "/api/v1/rfid/200",
        "put": "/api/v1/rfid/200",
        "delete": "/api/v1/rfid/200"
      }
    }
  ],
  "links": {
    "self": "/api/v1/rfid",
    "create": "/api/v1/rfid"
  }
}
```

* **POST** `/api/v1/rfid`

```json
{
  "sinal": "ABC123XYZ",
  "motoId": 100
}
```

* **PUT** `/api/v1/rfid/{id}`

```json
{
  "sinal": "XYZ789ABC",
  "motoId": 100
}
```

* **DELETE** `/api/v1/rfid/{id}`

---
