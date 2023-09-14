using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;

namespace Transportathon.Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(c => c.Id);

        builder.Property(company => company.Name)
            .HasMaxLength(200)
            .HasConversion(name => name.Value, value => new Name(value));
        
        builder.Property(company => company.Email)
            .HasMaxLength(200)
            .HasConversion(email => email.Value, value => new Email(value));

        builder.Property(company => company.Logo)
            .HasMaxLength(200)
            .HasConversion(logo => logo.Value, value => new Logo(value));
        
        builder.Property(company => company.Phone)
            .HasMaxLength(200)
            .HasConversion(phone => phone.Value, value => new Phone(value));
        
        

    }
}