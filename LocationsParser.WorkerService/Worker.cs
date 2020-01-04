using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LocationsParser.DataAccess.Entities;
using LocationsParser.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LocationsParser.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger,
                      IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope()) // for creating scoped service
                    {
                        var locationsService = scope.ServiceProvider.GetRequiredService<ILocationsService>();

                        await locationsService.PushDataToDatabase();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"{nameof(Worker)}.{nameof(ExecuteAsync)} threw an exception.");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}