using LocationsParser.Services;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationsParser.ConsoleCLI.Commands
{
    internal class GetAllCommand : CommandLineApplication, ICommand
    {
        private readonly ILocationsReaderService _locationsReaderService;

        public GetAllCommand(ILocationsReaderService locationsReaderService)
        {
            _locationsReaderService = locationsReaderService;
            Name = "getall";
            Description = "gets all locations";
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)Execute);
        }

        public int Execute()
        {
            var locations = _locationsReaderService.GetAllLocations();
            Helpers.PrintLocations(locations);
            return 0;
        }
    }
}
