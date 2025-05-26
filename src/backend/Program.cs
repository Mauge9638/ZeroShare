using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Services;
using Backend.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var dbConnectionString = builder.Configuration.GetConnectionString("DbConnection");
if (string.IsNullOrEmpty(dbConnectionString))
{
    throw new InvalidOperationException("Database connection string is not configured.");
}

builder.Services.AddDbContext<SnippetContext>(options =>
    options.UseNpgsql(dbConnectionString).UseSnakeCaseNamingConvention()
    );

builder.Services.AddScoped<ISnippetRepository, SnippetRepository>();
builder.Services.AddScoped<ISnippetService, SnippetService>();
builder.Services.AddHostedService<SnippetCleanupService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
