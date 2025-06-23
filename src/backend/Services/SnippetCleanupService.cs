using Backend.Repositories;

namespace Backend.Services
{
    public class SnippetCleanupService(IServiceProvider serviceProvider, ILogger<SnippetCleanupService> logger, IConfiguration configuration) : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ILogger<SnippetCleanupService> _logger = logger;
        private readonly TimeSpan _cleanupIntervalMinutes = configuration.GetValue<TimeSpan>("SnippetCleanup:IntervalCleanupMinutes", TimeSpan.FromMinutes(60));

        private readonly int _inactivityRetentionDays = configuration.GetValue<int>("SnippetCleanup:InactivityRetentionDays", 30);


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

                await Task.Delay(_cleanupIntervalMinutes, stoppingToken);
            }
        }

        private async Task CleanupExpiredSnippetsAsync()
        {
            _logger.LogInformation("Starting cleanup of expired snippets at {Time}", DateTime.UtcNow);

            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<ISnippetRepository>();

            var deletedExpiredCount = await repository.DeleteExpiredSnippetsAsync(DateTime.UtcNow);

            var deletedInactiveCount = await repository.DeleteInactiveSnippetsAsync(_inactivityRetentionDays);

            _logger.LogInformation("Cleaned up {Count} expired snippets at {Time}", deletedExpiredCount, DateTime.UtcNow);
        }
    }
}