using ServicoEstoque.Infrastructure;
using ServicoEstoque.Application;
using ServicoEstoque.Api.Extensions;
using ServicoEstoque.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services
    .AddInfrastructure()
    .AddApplication()
    .AddValidators()
    .AddMiddlewares();


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
        c.DocumentTitle = "API Estoque - V1";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Estoque - V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
