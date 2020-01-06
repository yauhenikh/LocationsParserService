using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationsParser.ConsoleCLI.Commands
{
    public class CommandLineApplicationWithDI : CommandLineApplication
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandLineApplicationWithDI(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RegisterCommands();
        }

        private void RegisterCommands()
        {
            foreach (var command in _serviceProvider.GetServices<ICommand>())
            {
                var commandLineApp = command as CommandLineApplication;

                if (commandLineApp == null)
                {
                    throw new InvalidCastException("Commands must inherit from ICommand and CommandLineApplication");
                }

                Commands.Add(commandLineApp);
            }
        }
    }
}
