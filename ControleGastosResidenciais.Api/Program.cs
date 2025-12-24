using ControleGastosResidenciais.Api.Configuration;
using ControleGastosResidenciais.Application.Common.Adapter;
using ControleGastosResidenciais.Application.Common.Adapter.Interface;
using ControleGastosResidenciais.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.SQLite(
        sqliteDbPath: "controle_gastos.db",
        tableName: "Logs",
        storeTimestampInUtc: true
    )
    .CreateLogger();

try
{
    Log.Information("Starting ControleGastosResidenciais aplication");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


    builder.Services.AddControllers();
    builder.Services.AddVersioning();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
               .AddValidators()
               .AddServices()
               .AddRepositories()
               .AddScoped<IPersonAdapter, PersonAdapter>()
               .AddScoped<ICategoryAdapter, CategoryAdapter>()
               .AddScoped<ITransactionAdapter, TransactionAdapter>();

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    var app = builder.Build();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplicação falhou ao iniciar.");
}
finally
{
    Log.CloseAndFlush();
}