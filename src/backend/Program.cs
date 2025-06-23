using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Services;
using Backend.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

    options.KnownProxies.Clear();
    options.KnownNetworks.Clear();

    if (!builder.Environment.IsDevelopment())
    {
        options.KnownNetworks.Add(new IPNetwork(System.Net.IPAddress.Parse("172.16.0.0"), 12));
        options.KnownNetworks.Add(new IPNetwork(System.Net.IPAddress.Parse("192.168.0.0"), 16));
        options.KnownNetworks.Add(new IPNetwork(System.Net.IPAddress.Parse("10.0.0.0"), 8));
        options.ForwardLimit = 1; // Security: limit forwarding depth
    }
    else
    {
        options.ForwardLimit = null;
    }
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRateLimiter(options =>
{
    var requestsPerMinute = builder.Configuration.GetValue<int>("RateLimit:RequestsPerMinute", 15);

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        var clientIP = context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

        if (builder.Environment.IsDevelopment())
        {
            Console.WriteLine($"Rate limiting for IP: {clientIP}");
        }

        return RateLimitPartition.GetSlidingWindowLimiter(
            partitionKey: clientIP,
            factory: partition => new SlidingWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                SegmentsPerWindow = 6,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            });
    });

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        context.HttpContext.Response.ContentType = "text/plain";
        await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Please try again later.", token);
    };
});

// Environment-specific CORS configuration
if (builder.Environment.IsDevelopment())
{
    // Development: Allow localhost origins
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins(
                    "http://localhost:5173",
                    "http://localhost:3000",
                    "http://localhost:8080"
                )
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

app.UseForwardedHeaders();


if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
    app.MapOpenApi();

    app.MapGet("/debug/ip", (HttpContext context) =>
{
    return new
    {
        RemoteIpAddress = context.Connection.RemoteIpAddress?.ToString(),
        XForwardedFor = context.Request.Headers["X-Forwarded-For"].ToString(),
        XRealIP = context.Request.Headers["X-Real-IP"].ToString(),
        AllHeaders = context.Request.Headers
            .Where(h => h.Key.StartsWith("X-"))
            .ToDictionary(h => h.Key, h => h.Value.ToString())
    };
});
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.UseRateLimiter();

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
