using LocationsParser.DataAccess.Data;
using LocationsParser.DataAccess.Entities;
using LocationsParser.Services.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocationsParser.Services
{
    public class LocationsFileService : ILocationsService
    {
        private readonly AppDbContext _dbContext;
        private readonly string _filePath;
        private readonly ILogger<LocationsFileService> _logger;

        public LocationsFileService(AppDbContext dbContext,
                                    IOptions<LocationsFileServiceConfiguration> configuration,
                                    ILogger<LocationsFileService> logger)
        {
            _dbContext = dbContext;
            _filePath = configuration == null ? throw new ArgumentNullException(nameof(configuration)) : configuration.Value.FilePath;
            _logger = logger;
        }

        public async Task PushDataToDatabase()
        {
            var locationsDTO = GetLocationsFromSource();
            var recordCount = 0;

            foreach (var locationDTO in locationsDTO)
            {
                try
                {
                    var city = new City
                    {
                        Name = LocationStringParserService.GetCityName(locationDTO)
                    };
                    if (_dbContext.Cities.Where(c => c.Name == city.Name).Count() == 0)
                    {
                        _dbContext.Cities.Add(city);
                        await _dbContext.SaveChangesAsync();

                        _logger.LogInformation($"Pushed city {city.Name} to database");
                    }

                    var cityId = _dbContext.Cities.Single(c => c.Name == city.Name).Id;

                    var location = new Location
                    {
                        UniqueIdentifier = locationDTO.Id,
                        Name = locationDTO.Name,
                        Email = locationDTO.Address,
                        OfficeName = LocationStringParserService.GetOfficeName(locationDTO),
                        CityId = cityId
                    };
                    if (_dbContext.Locations.Where(l => l.Email == location.Email).Count() == 0)
                    {
                        _dbContext.Locations.Add(location);
                        await _dbContext.SaveChangesAsync();

                        _logger.LogInformation($"Pushed location {location.Name} to database");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, $"{nameof(LocationsFileService)}.{nameof(PushDataToDatabase)} threw an exception at record number {recordCount}.");
                }

                recordCount++;
            }
        }

        private List<LocationDTO> GetLocationsFromSource()
        {
            var jsonData = File.ReadAllText(_filePath);
            var locations = JsonConvert.DeserializeObject<List<LocationDTO>>(jsonData);

            return locations;
        }
    }
}
