using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon.Domain.Transports;

namespace Transportathon.Infrastructure.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);
        
        builder.Property(company => company.NumberPlate)
            .HasMaxLength(50)
            .HasConversion(plate => plate.Value, value => new NumberPlate(value));
        
        builder.Property(company => company.Color)
            .HasMaxLength(50)
            .HasConversion(color => color.Value, value => new Color(value));
        
        
        builder.Property(company => company.Model)
            .HasMaxLength(50)
            .HasConversion(model => model.Value, value => new VehicleModel(value));
        
        builder.Property(company => company.Year)
            .HasMaxLength(50)
            .HasConversion(year => year.Value, value => new Year(value));

    }
}