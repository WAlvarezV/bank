using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Client.Persistence.Context.EntitiesConfiguration
{
    internal class ClientConfig : IEntityTypeConfiguration<Domain.Entities.Client>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Client> builder)
        {
            var name = nameof(Domain.Entities.Client);
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.ClientId)
                .HasDatabaseName("UK_ClientId")
                .IsUnique();

            builder.Property(p => p.ClientId)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment($"client id of the {name}");
        }
    }
}
