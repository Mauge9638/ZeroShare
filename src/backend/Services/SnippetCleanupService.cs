using Backend.Repositories;

namespace Backend.Services
{
    public class SnippetCleanupService(IServiceProvider serviceProvider, ILogger<SnippetCleanupService> logger) : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ILogger<SnippetCleanupService> _logger = logger;
        private readonly TimeSpan _period = TimeSpan.FromMinutes(5);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CleanupExpiredSnippetsAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while cleaning up expired snippets");
                }

                await Task.Delay(_period, stoppingToken);
            }
        }

        private async Task CleanupExpiredSnippetsAsync()
        {
            _logger.LogInformation("Starting cleanup of expired snippets at {Time}", DateTime.UtcNow);

            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<ISnippetRepository>();

            var deletedCount = await repository.DeleteExpiredSnippetsAsync(DateTime.UtcNow);

            _logger.LogInformation("Cleaned up {Count} expired snippets at {Time}", deletedCount, DateTime.UtcNow);
        }
    }
}