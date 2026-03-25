using Microsoft.EntityFrameworkCore;
using SecurePaymentService.Data;
using SecurePaymentService.Middleware;
using SecurePaymentService.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona o suporte para Controllers
builder.Services.AddControllers();

// 2. Configura o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o nosso novo serviço de pagamento
builder.Services.AddScoped<IPaymentService, PaymentService>();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>(); 
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
// 3. Ativa o Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// 4. Mapeia os Controllers (ele vai procurar automaticamente na pasta Controllers)
app.MapControllers();
app.Run();