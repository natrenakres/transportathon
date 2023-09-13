using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;

namespace Transportathon.Infrastructure.Configurations;

public class CarrierConfiguration : IEntityTypeConfiguration<Carrier>
{
    public void Configure(EntityTypeBuilder<Carrier> builder)
    {
        builder.ToTable("Carriers");

        builder.HasKey(c => c.Id);
        
        builder.Property(company => company.Experience)
            .HasMaxLength(50)
            .HasConversion(experience => experience.Value, value => new Year(value));
        
        builder.Property(company => company.Name)
            .HasMaxLength(50)
            .HasConversion(name => name.Value, value => new Name(value));
    }
}