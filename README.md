# Secure Payment Microservice
Este é um microserviço de processamento de pagamentos desenvolvido em .NET 8, focado em segurança de dados, Clean Code e TDD (Test Driven Development). O projeto demonstra um fluxo robusto, desde a validação algorítmica de cartões até a persistência segura e testada.

# Tecnologias e Ferramentas
Backend: .NET 8 (ASP.NET Core Web API)
Banco de Dados: PostgreSQL (Persistência Real) & EF Core InMemory (para Testes)
ORM: Entity Framework Core (Migrations)
Testes: xUnit, Moq e FluentAssertions
Documentação: Swagger/OpenAPI
Segurança: Algoritmo de Luhn (Mod10) e Sanitização de Logs

# Arquitetura e Boas Práticas
O projeto foi construído seguindo princípios de SOLID e Clean Code, destacando-se por:
Service Pattern: Lógica de negócio isolada dos Controllers.
Data Transfer Objects (DTOs): Contratos de entrada e saída bem definidos, evitando exposição de entidades de banco.
Tratamento Global de Exceções: Custom Middleware para padronização de erros e limpeza de blocos try-catch.
Shared Kernel: Centralização de Enums e Constantes para eliminar "Magic Strings".
Fail-Fast Validation: Uso de Data Annotations para validar requisições antes do processamento.

# Qualidade de Código (Testes)
Este projeto utiliza Testes Unitários para garantir a integridade das regras de negócio:

1. Validação de Luhn: Testes que garantem o bloqueio de cartões inválidos.
2. Persistência: Testes de integração utilizando banco em memória para validar o salvamento de transações.
3. Padronização: Verificação de mensagens de erro e status de retorno.

# Como Executar
Pré-requisitos
.NET 8 SDK
PostgreSQL (ou ajuste a string de conexão para outro provider)

# Instalação
Clone o repositório: git clone https://github.com/seu-usuario/secure-payment-microservice.git
Configure a DefaultConnection no appsettings.json.
Execute as migrations: dotnet ef database update.
Inicie a API: dotnet run.

# Rodando os Testes
Para validar a segurança e o funcionamento do sistema, execute:

    Bash
    dotnet test

# Próximos Passos
[ ] Implementação do Repository Pattern para abstração total do banco.

[ ] Autenticação via JWT para proteção dos endpoints.

[ ] Implementação de Docker para orquestração do microserviço e banco.