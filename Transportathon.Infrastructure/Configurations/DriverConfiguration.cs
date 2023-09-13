using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;

namespace Transportathon.Infrastructure.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");

        builder.HasKey(d => d.Id);
        
        builder.Property(company => company.Experience)
            .HasMaxLength(50)
            .HasConversion(experience => experience.Value, value => new Experience(value));
        
        builder.Property(company => company.Name)
            .HasMaxLength(50)
            .HasConversion(name => name.Value, value => new Name(value));
    }
}