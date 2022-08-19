using Bank.Common.Application.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bank.Account.Persistence.Context.EntitiesConfiguration
{
    internal class AccountConfig : IEntityTypeConfiguration<Domain.Entities.Account>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Account> builder)
        {
            var name = nameof(Domain.Entities.Account);
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Number)
                .HasDatabaseName("UK_Number")
                .IsUnique();

            builder.Property(p => p.ClientId)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment($"client id of the {name}");

            var converter = new EnumToStringConverter<AccountEnum>();
            builder.Property(p => p.AccountType).HasMaxLength(20).HasConversion(converter);
        }
    }
}
