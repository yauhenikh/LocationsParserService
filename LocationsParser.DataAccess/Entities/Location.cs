using System;
using System.Collections.Generic;
using System.Text;

namespace LocationsParser.DataAccess.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string UniqueIdentifier { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OfficeName { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
