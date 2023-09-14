using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;

namespace Transportathon.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(user => user.Name)
            .HasMaxLength(200)
            .HasConversion(name => name.Value, value => new Name(value));
        
        builder.Property(user => user.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => new Email(value)); 
        
        
        builder.Property(user => user.Phone)
            .HasMaxLength(50)
            .HasConversion(phone => phone.Value, value => new Phone(value)); 

        builder.HasIndex(user => user.Email).IsUnique();

       


    }
}