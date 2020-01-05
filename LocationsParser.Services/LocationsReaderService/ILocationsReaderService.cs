using LocationsParser.DataAccess.Entities;
using System.Collections.Generic;

namespace LocationsParser.Services
{
    public interface ILocationsReaderService
    {
        List<Location> GetAllLocations();
        Location GetLocationByUniqueId(string uniqueId);
        Location GetLocationByEmail(string email);
        List<Location> GetLocationsByCity(string city);
    }
}