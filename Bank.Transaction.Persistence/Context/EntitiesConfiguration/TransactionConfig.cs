using Bank.Common.Application.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bank.Transaction.Persistence.Context.EntitiesConfiguration
{
    internal class TransactionConfig : IEntityTypeConfiguration<Domain.Entities.Transaction>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Transaction> builder)
        {
            var name = nameof(Domain.Entities.Transaction);
            builder.HasKey(p => p.Id);

            builder.Property(p => p.AccountId)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment($"Account id of the {name}");

            var converter = new EnumToStringConverter<TransactionEnum>();
            builder.Property(p => p.TransactionType).HasMaxLength(20).HasConversion(converter);
        }
    }
}
