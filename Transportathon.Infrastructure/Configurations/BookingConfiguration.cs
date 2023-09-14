using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon.Domain.Bookings;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;

namespace Transportathon.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);
        
        builder.Property<uint>("Version").IsRowVersion();

        builder.HasOne<TransportRequest>()
            .WithMany()
            .HasForeignKey(booking => booking.RequestId);

        builder.HasOne<Company>()
            .WithMany()
            .HasForeignKey(booking => booking.CompanyId);

        builder.HasOne<Carrier>()
            .WithMany()
            .HasForeignKey(booking => booking.CarrierId);

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(booking => booking.VehicleId);


    }
}