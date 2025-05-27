using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Services;
using Backend.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Environment-specific CORS configuration
if (builder.Environment.IsDevelopment())
{
    // Development: Allow localhost origins
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Might need this for dev tools
        });
    });
}
else
{
    // Production: Allow specific domains
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins("https://zeroshare.app", "https://www.zeroshare.app")
                  .AllowAnyHeader()
                  .AllowAnyMethod();

        });
    });
}

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

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
