using LocationsParser.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationsParser.ConsoleCLI.Commands
{
    internal static class Helpers
    {
        internal static void PrintLocation(Location location)
        {
            if (location == null)
                return;
            Console.WriteLine($"Name: {location.Name}, Email: {location.Email}");
        }

        internal static void PrintLocations(List<Location> locations)
        {
            locations.ForEach(PrintLocation);
        }
    }
}
