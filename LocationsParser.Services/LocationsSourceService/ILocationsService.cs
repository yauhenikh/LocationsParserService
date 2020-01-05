using LocationsParser.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationsParser.Services
{
    public interface ILocationsService
    {
        Task PushDataToDatabase();
    }
}
