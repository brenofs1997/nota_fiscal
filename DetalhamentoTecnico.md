Detalhamento Técnico - Sistema de Emissão de Notas Fiscais
Este documento descreve as decisões arquiteturais, padrões de design e tecnologias implementadas no ecossistema do projeto (Serviços de Faturamento e Estoque).

1. Frontend: Arquitetura e Ciclo de Vida (Angular)
O desenvolvimento utilizou o framework Angular 17+, focando em componentes desacoplados e performance.

Ciclos de Vida (Lifecycle Hooks)
ngOnInit: Centralização da lógica de inicialização, como o setup de formulários reativos e o disparo de requisições iniciais para carregamento de grades (produtos e notas).

ngOnDestroy: Garantia de memória e performance através do cancelamento de Subscriptions ativas do RxJS, prevenindo memory leaks.

ngOnChanges: Reatividade de componentes filhos (como diálogos de confirmação) baseada na mutação de decoradores @Input().

ngAfterViewInit: Manipulação refinada do DOM e integração com componentes do Angular Material que dependem da renderização completa da View (ex: MatPaginator e MatSort).

2. Programação Reativa e Estado (RxJS)
A aplicação utiliza o RxJS para gerenciar a assincronia de forma declarativa.

Operadores e Estratégias
HttpClient: Interface nativa para consumo de APIs REST retornando Observables.

BehaviorSubject: Utilizado para o gerenciamento de estado compartilhado entre componentes (ex: atualização do saldo de estoque após faturamento).

Pipeable Operators:

map: Transformação de DTOs da API para modelos de visualização.

catchError: Interceptação e tratamento centralizado de falhas.

switchMap: Otimização de busca, cancelando requisições obsoletas em favor da mais recente.

debounceTime: Redução de overhead no servidor em campos de busca type-ahead.

retry: Implementação de resiliência em falhas transientes de rede.

3. Stack Tecnológica e Dependências
Frontend (SPA)
Core: @angular/core, @angular/forms (Reactive), @angular/router.

UI/UX: Angular Material (MatTable, MatDialog, MatSnackBar).

Comunicação: @angular/common/http.

Backend (Microsserviços .NET 10)
Runtime: ASP.NET Core Web API.

ORM: Entity Framework Core 10+ (Code First).

Database: PostgreSQL (via Npgsql).

Documentação: Swashbuckle (Swagger/OpenAPI).

Validação: FluentValidation para garantir integridade dos modelos de entrada.

4. Arquitetura de Backend e Persistência
Framework e Design Patterns
Dependency Injection (DI): Uso extensivo do contêiner nativo para desacoplamento de camadas (Application, Domain, Infrastructure).

Repository & Unit of Work: Implementação para abstração da camada de dados e garantia de atomicidade em transações complexas (ex: cadastro de nota e baixa de estoque).

Middleware Pipeline: Tratamento global de exceções e logs de auditoria.

Gerenciamento de Banco de Dados
EF Core Migrations: Versionamento controlado do esquema do banco de dados.

Concorrência Otimista: Uso de Concurrency Tokens para prevenir a sobreposição de dados em atualizações simultâneas de estoque.

5. Resiliência, Erros e Idempotência
Estratégias de Tratamento de Erros
Status Codes Semânticos:

400 Bad Request: Falhas de validação de negócio.

422 Unprocessable Content: Erros semânticos (ex: saldo insuficiente).

500 Internal Server Error: Tratamento genérico via Middleware para segurança da informação.

Domain Notification: Uso de um Notifier para capturar erros de domínio sem interromper o fluxo da aplicação com exceções custosas.

Idempotência e Segurança
Idempotency Keys: Implementação de chaves únicas enviadas pelo cliente para evitar a duplicidade de Notas Fiscais em caso de reenvios da requisição.

Transaction Rollback: Garantia de integridade; se o serviço de estoque falhar, a nota fiscal não é persistida como "Fechada".

6. Comunicação entre Microsserviços
A integração entre o Serviço de Faturamento e o Serviço de Estoque ocorre via:

Chamadas HTTP Assíncronas: Utilização de IHttpClientFactory para comunicação resiliente.

Eventual Consistency: Verificação de disponibilidade e sincronização de saldos de produto durante o fechamento de notas.