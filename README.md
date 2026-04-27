# 🚀 TaskManager - Fullstack (.NET 6 + React)

API RESTful com frontend em React para gerenciamento de tarefas, utilizando arquitetura em camadas e boas práticas de desenvolvimento.

---

## 📌 Descrição

Aplicação CRUD completa para gerenciamento de tarefas por usuário, desenvolvida com foco em:

* Separação de responsabilidades
* Uso de DTOs para desacoplamento
* Arquitetura limpa (Controller → Service → Repository)
* Integração fullstack (API + Frontend)

---

## 🛠️ Tecnologias Utilizadas

### Backend

* .NET 6 (SDK 6.0)
* Entity Framework Core 6.x
* MySQL 8.0
* AutoMapper 12.x
* Swagger (Swashbuckle 6.5.0)

### Frontend

* React 18.x
* Axios 1.x
* Vite 5.x

### Infraestrutura

* Docker 24.x+
* Docker Compose v2
* Nginx (alpine)

---

## 📦 Dependências

### NuGet (Backend)

* Swashbuckle.AspNetCore 6.x
* Swashbuckle.AspNetCore.Annotations (6.5.0)
* AutoMapper.Extensions.Microsoft.DependencyInjection 12.x
* Pomelo.EntityFrameworkCore.MySql 6.x

### NPM (Frontend)

* axios 1.x
* react 18.x
* vite 5.x

---

## 🚀 Como rodar o projeto

### 🔹 1. Subir tudo com Docker

```bash
docker-compose up -d --build
```

---

### 🔹 2. Acessar aplicação

* Frontend → http://localhost:3000
* API (Swagger) → http://localhost:5000/swagger
* Banco MySQL → porta 3307

---

## 🔄 Fluxo da Aplicação

```text
React → Axios → Controller → Service → Repository → MySQL
```

Retorno:

```text
MySQL → Repository → Service → DTO → Controller → React
```

---

## 🧠 Arquitetura

* **Controller** → expõe endpoints REST e retorna status HTTP
* **Service** → contém regras de negócio
* **Repository** → acesso ao banco via EF Core
* **DTOs** → evitam exposição direta das entidades

---

## 📡 Endpoints principais

### User

* `GET /api/User`
* `POST /api/User`
* `GET /api/User/{id}`
* `DELETE /api/User/{id}`

### Task

* `GET /api/Task`
* `POST /api/Task`
* `PUT /api/Task/{id}`
* `PATCH /api/Task/{id}`
* `DELETE /api/Task/{id}`

---

## 🛡️ Tratamento de erros

A aplicação utiliza middleware global para captura de exceções, retornando respostas padronizadas:

```json
{
  "statusCode": 404,
  "message": "Tarefa não encontrada"
}
```

---

## 📌 Observações

* O banco é inicializado automaticamente via Docker
* Pode haver pequeno delay na primeira execução (subida do MySQL)
