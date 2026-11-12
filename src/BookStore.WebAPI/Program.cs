using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection:
// Domain interfaces -> Infrastructure implementations
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();

// Application interfaces -> Application services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Redirect root to swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();
app.Run();
