using LocationsParser.DataAccess.Entities;
using LocationsParser.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationsParser.Services
{
    public static class LocationStringParserService
    {
        private const string _cityPrefix = "Rooms ";
        private const string _officeNamePrefix = " ";
        private const char _separator = ',';
        
        public static string GetCityName(LocationDTO locationDTO)
        {
            var cityStringWithPrefix = locationDTO.Name.Split(_separator)[0];

            if (cityStringWithPrefix.Length < _cityPrefix.Length ||
                cityStringWithPrefix.Substring(0, _cityPrefix.Length) != _cityPrefix)
            {
                throw new ArgumentException($"Impossible to get city name from \"{locationDTO.Name}\"");
            }

            return cityStringWithPrefix.Substring(_cityPrefix.Length);
        }

        public static string GetOfficeName(LocationDTO locationDTO)
        {
            var parts = locationDTO.Name.Split(_separator);

            if (parts.Length < 2)
                return null;

            var officeNameStringWithPrefix = parts[1];

            if (officeNameStringWithPrefix.Length < _officeNamePrefix.Length ||
                officeNameStringWithPrefix.Substring(0, _officeNamePrefix.Length) != _officeNamePrefix)
            {
                throw new ArgumentException($"Impossible to get office name from \"{locationDTO.Name}\"");
            }

            return officeNameStringWithPrefix.Substring(_officeNamePrefix.Length);
        }
    }
}
