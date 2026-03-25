using Microsoft.EntityFrameworkCore;
using SecurePaymentService.Data;
using SecurePaymentService.Middleware;
using SecurePaymentService.Services;

var builder = WebApplication.CreateBuilder(args);

// --- SERVIÇOS (Configuração) ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Banco de Dados (PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de Injeção de Dependência (Fundamental para a arquitetura!)
builder.Services.AddScoped<IPaymentService, PaymentService>();

var app = builder.Build();

// --- MIDDLEWARES (Execução) ---

// 1. Rede de proteção global (deve vir primeiro!)
app.UseMiddleware<ExceptionMiddleware>(); 

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. Mapeamento de Rotas
app.MapControllers();

app.Run();