using LocationsParser.DataAccess.Entities;
using LocationsParser.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationsParser.Services
{
    public static class LocationStringParserService
    {
        public static string GetCityName(LocationDTO locationDTO)
        {
            return locationDTO.Name.Split(',')[0].Substring("Rooms ".Length);
        }

        public static string GetOfficeName(LocationDTO locationDTO)
        {
            var parts = locationDTO.Name.Split(',');

            if (parts.Length < 2)
                return null;

            return parts[1].Substring(" ".Length);
        }
    }
}
