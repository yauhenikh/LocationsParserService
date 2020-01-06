using LocationsParser.Services;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationsParser.ConsoleCLI.Commands
{
    internal class GetByUniqueIdCommand : CommandLineApplication, ICommand
    {
        private readonly ILocationsReaderService _locationsReaderService;
        private readonly CommandArgument _argument;

        public GetByUniqueIdCommand(ILocationsReaderService locationsReaderService)
        {
            _locationsReaderService = locationsReaderService;
            Name = "getbyuniqueid";
            _argument = Argument("uniqueId", "unique id of location").IsRequired();
            Description = "gets location by unique id";
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)Execute);
        }

        public int Execute()
        {
            var location = _locationsReaderService.GetLocationByUniqueId(_argument.Value);
            Helpers.PrintLocation(location);
            return 0;
        }
    }
}
