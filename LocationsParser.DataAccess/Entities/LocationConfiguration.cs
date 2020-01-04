using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationsParser.DataAccess.Entities
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);
            builder.HasOne(l => l.City).WithMany(c => c.Locations).HasForeignKey(l => l.CityId);
            builder.Property(l => l.Name).IsRequired();
            builder.Property(l => l.Email).IsRequired();
        }
    }
}
