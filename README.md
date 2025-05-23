
# OdontoGuardAPI

## Descrição

**OdontoGuardAPI** é uma API desenvolvida em **ASP.NET Core** que oferece funcionalidades de gerenciamento de usuários e análise de sentimentos utilizando **ML.NET**. A API é capaz de realizar operações CRUD para usuários e classificar sentimentos a partir de um texto fornecido.

## Tecnologias Utilizadas

- **ASP.NET Core 6**: Framework para desenvolvimento de APIs.
- **ML.NET**: Biblioteca para machine learning utilizada para análise de sentimentos.
- **Swagger**: Documentação interativa da API.
- **Oracle Database**: Banco de dados utilizado para armazenamento dos usuários.
- **JWT (JSON Web Token)**: Implementação de autenticação para proteger os endpoints.

## Como Rodar o Projeto

### Requisitos

- **.NET 6 ou superior** instalado na sua máquina.
- **Oracle Database**: Caso esteja usando Oracle, configure a conexão no `appsettings.json`.

### Passos para Executar

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/seu-usuario/odontoguardapi.git
   cd odontoguardapi
   ```

2. **Instale as dependências**:
   ```bash
   dotnet restore
   ```

3. **Rodando o Projeto**:
   Para rodar a API localmente, use o comando:
   ```bash
   dotnet run
   ```

4. A API estará disponível em **`http://localhost:5008`**. Você pode acessar a documentação interativa do Swagger em **`http://localhost:5008/swagger`**.

### Configuração do Banco de Dados (Oracle)

A API utiliza **Oracle** para armazenamento dos usuários. Certifique-se de configurar a string de conexão corretamente em **`appsettings.json`**:

```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=usuario;Password=senha;Data Source=localhost:1521/xe;"
  }
}
```

### Configuração de JWT

A API utiliza **JWT** para autenticação. A chave **`Jwt:Key`** deve ser configurada no **`appsettings.json`**:

```json
{
  "Jwt": {
    "Key": "secrect_key_12345",
    "Issuer": "OdontoGuardAPI",
    "Audience": "OdontoGuardUsers"
  }
}
```

## Endpoints da API

### 1. **GET /api/usuario**

Listar todos os usuários cadastrados.

**Exemplo de resposta**:
```json
[
  {
    "id": 1,
    "nome": "João Silva",
    "email": "joao@exemplo.com",
    "senha": "senha123"
  },
  {
    "id": 2,
    "nome": "Maria Oliveira",
    "email": "maria@exemplo.com",
    "senha": "senha456"
  }
]
```

### 2. **POST /api/usuario**

Criar um novo usuário.

**Exemplo de corpo da requisição**:
```json
{
  "nome": "João Silva",
  "email": "joao@exemplo.com",
  "senha": "senha123"
}
```

**Exemplo de resposta**:
```json
{
  "id": 1,
  "nome": "João Silva",
  "email": "joao@exemplo.com",
  "senha": "senha123"
}
```

### 3. **GET /api/usuario/{id}**

Buscar um usuário pelo ID.

**Exemplo de resposta**:
```json
{
  "id": 1,
  "nome": "João Silva",
  "email": "joao@exemplo.com",
  "senha": "senha123"
}
```

### 4. **PUT /api/usuario/{id}**

Atualizar os dados de um usuário.

**Exemplo de corpo da requisição**:
```json
{
  "nome": "João Silva Atualizado",
  "email": "joao_atualizado@exemplo.com",
  "senha": "nova_senha123"
}
```

### 5. **DELETE /api/usuario/{id}**

Deletar um usuário pelo ID.

**Exemplo de resposta**: **204 No Content** (sem corpo).

### 6. **POST /api/sentiment**

Analisar o sentimento de um texto.

**Exemplo de corpo da requisição**:
```json
{
  "text": "Estou muito feliz com o serviço!"
}
```

**Exemplo de resposta**:
```json
{
  "isPositive": true
}
```


## Notas

- A API está configurada para funcionar com **Oracle** como banco de dados.
- A análise de sentimentos é feita com **ML.NET**, utilizando um modelo simples de classificação binária (positivo ou negativo).
