using System;
using System.Collections.Generic;
using System.Text;

namespace LocationsParser.DataAccess.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
