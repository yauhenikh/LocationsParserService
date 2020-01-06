using LocationsParser.Services;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationsParser.ConsoleCLI.Commands
{
    internal class GetByEmailCommand : CommandLineApplication, ICommand
    {
        private readonly ILocationsReaderService _locationsReaderService;
        private readonly CommandArgument _argument;

        public GetByEmailCommand(ILocationsReaderService locationsReaderService)
        {
            _locationsReaderService = locationsReaderService;
            Name = "getbyemail";
            _argument = Argument("email", "unique email of location").IsRequired();
            Description = "gets location by unique email";
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)Execute);
        }

        public int Execute()
        {
            var location = _locationsReaderService.GetLocationByEmail(_argument.Value);
            Helpers.PrintLocation(location);
            return 0;
        }
    }
}
