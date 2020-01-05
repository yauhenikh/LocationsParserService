using LocationsParser.DataAccess.Data;
using LocationsParser.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationsParser.Services
{
    public class LocationsDbReaderService : ILocationsReaderService
    {
        private readonly AppDbContext _dbContext;

        public LocationsDbReaderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Location> GetAllLocations()
        {
            return _dbContext.Locations.ToList();
        }

        public Location GetLocationByUniqueId(string uniqueId)
        {
            return _dbContext.Locations.Where(l => l.UniqueIdentifier == uniqueId).SingleOrDefault();
        }

        public Location GetLocationByEmail(string email)
        {
            return _dbContext.Locations.Where(l => l.Email == email).FirstOrDefault();
        }

        public List<Location> GetLocationsByCity(string city)
        {
            return _dbContext.Locations.Where(l => l.City.Name == city).ToList();
        }
    }
}
