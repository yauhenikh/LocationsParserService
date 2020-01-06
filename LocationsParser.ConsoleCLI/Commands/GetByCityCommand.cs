using LocationsParser.Services;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationsParser.ConsoleCLI.Commands
{
    internal class GetByCityCommand : CommandLineApplication, ICommand
    {
        private readonly ILocationsReaderService _locationsReaderService;
        private readonly CommandArgument _argument;

        public GetByCityCommand(ILocationsReaderService locationsReaderService)
        {
            _locationsReaderService = locationsReaderService;
            Name = "getbycity";
            _argument = Argument("city", "city of location").IsRequired();
            Description = "gets locations by city";
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)Execute);
        }

        public int Execute()
        {
            var locations = _locationsReaderService.GetLocationsByCity(_argument.Value);
            Helpers.PrintLocations(locations);
            return 0;
        }
    }
}
