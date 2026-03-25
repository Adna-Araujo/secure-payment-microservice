#  Secure Payment Microservice

Este é um microserviço de processamento de pagamentos desenvolvido em **.NET 8**, focado em segurança de dados, arquitetura limpa e resiliência. O projeto demonstra o fluxo completo desde a validação de cartões até a persistência em banco de dados relacional.

##  Tecnologias e Padrões
- **Framework:** .NET 8 (ASP.NET Core Web API)
- **Banco de Dados:** PostgreSQL
- **ORM:** Entity Framework Core (Migrations)
- **Documentação:** Swagger/OpenAPI
- **Segurança:** Implementação do Algoritmo de Luhn (Mod10)
- **Arquitetura:** Padrão DTO (Data Transfer Objects) e Middleware Global de Erros

##  Diferenciais do Projeto
- **Validação Algorítmica:** Proteção contra números de cartões inválidos antes mesmo de atingir o banco.
- **Privacidade por Design:** Mascaramento automático de dados sensíveis em respostas de API.
- **Tratamento de Exceções:** Middleware customizado para garantir respostas JSON padronizadas mesmo em falhas críticas.
- **Persistência Real:** Diferente de projetos em memória, este utiliza um banco relacional real para armazenamento.

##  Como Executar
1. Clone o repositório.
2. Configure sua string de conexão no `appsettings.json`.
3. Execute as migrações para criar o banco: `dotnet ef database update`.
4. Inicie a aplicação: `dotnet run`.


<img width="1240" height="693" alt="image" src="https://github.com/user-attachments/assets/dc376f03-7c5e-4791-9c0f-a71e14c47cb7" />


##  Próximos Passos

[ ] Implementação de Testes Unitários com xUnit.

[ ] Adição de Injeção de Dependência com Repository Pattern.

[ ] Autenticação via JWT para proteger os endpoints.