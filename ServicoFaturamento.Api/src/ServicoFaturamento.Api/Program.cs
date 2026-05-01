using ServicoFaturamento.Application;
using ServicoFaturamento.Core.Interfaces.Services;
using ServicoFaturamento.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services
    .AddInfrastructure()
    .AddApplication();

builder.Services.AddHttpClient<IEstoqueIntegrationService, EstoqueIntegrationService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:EstoqueUrl"]);
});


builder.Services.AddOpenApi();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular", policy => {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "API Emissor N.F - V1 ";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Emissor N.F - V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
