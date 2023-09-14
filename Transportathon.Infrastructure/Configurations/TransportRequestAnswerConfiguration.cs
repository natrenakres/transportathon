using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;

namespace Transportathon.Infrastructure.Configurations;

public class TransportRequestAnswerConfiguration : IEntityTypeConfiguration<TransportRequestAnswer>
{
    public void Configure(EntityTypeBuilder<TransportRequestAnswer> builder)
    {
        builder.ToTable("TransportRequestAnswers");

        builder.HasKey(a => a.Id);
        
        builder.OwnsOne(answer => answer.Price, priceBuilder =>
        {
            priceBuilder.Property(money => money.Amount)
                .HasColumnType("decimal(18,4)");
            
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.HasOne<TransportRequest>()
            .WithMany()
            .HasForeignKey(k => k.RequestId);
        
        builder.Property<uint>("Version").IsRowVersion();
    }
}