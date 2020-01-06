using LocationsParser.ConsoleCLI.Commands;
using LocationsParser.DataAccess.Entities;
using LocationsParser.Services;
using LocationsParser.WorkerService;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace LocationsParser.ConsoleCLI
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            AddConfiguration();
            RegisterServices();

            var commandLineApp = new CommandLineApplicationWithDI(_serviceProvider);
            commandLineApp.HelpOption();

            try
            {
                commandLineApp.Execute(args);
            }
            catch (UnrecognizedCommandParsingException)
            {
                Console.Write("Unknown command ");
                foreach (var arg in args)
                {
                    Console.Write($"{arg} ");
                }
                Console.WriteLine();
            }
        }

        private static void AddConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext(_configuration);
            services.AddScoped<ILocationsReaderService, LocationsDbReaderService>();
            services.AddSingleton<ICommand, GetAllCommand>();
            services.AddSingleton<ICommand, GetByUniqueIdCommand>();
            services.AddSingleton<ICommand, GetByEmailCommand>();
            services.AddSingleton<ICommand, GetByCityCommand>();

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
