using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocationsParser.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LocationsParser.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext(hostContext.Configuration);
                    services.AddScoped<ILocationsService, LocationsFileService>();
                    services.Configure<LocationsFileServiceConfiguration>(hostContext.Configuration.GetSection("LocationsFileServiceConfiguration"));
                    services.AddHostedService<Worker>();
                });
    }
}
