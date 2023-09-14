using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;

namespace Transportathon.Infrastructure.Configurations;

public class TransportRequestConfiguration : IEntityTypeConfiguration<TransportRequest>
{
    public void Configure(EntityTypeBuilder<TransportRequest> builder)
    {
        builder.ToTable("TransportRequests");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(r => r.Address);
        
        builder.OwnsOne(request => request.Price, priceBuilder =>
        {
            priceBuilder.Property(money => money.Amount)
                .HasColumnType("decimal(18,4)");
            
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });
        
        builder.Property(apartment => apartment.Description)
            .HasMaxLength(2000)
            .HasConversion(description => description.Value, value => new Description(value));

        
        
        builder.Property<uint>("Version").IsRowVersion();
    }
}