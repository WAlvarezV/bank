using Bank.Client.Domain.Entities;
using Bank.Common.Application.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bank.Client.Persistence.Context.EntitiesConfiguration
{
    internal class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            var name = nameof(Person);
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Identification)
                .HasDatabaseName("UK_Identification")
                .IsUnique();

            builder.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(200)
                .HasComment($"{name} name");

            var converter = new EnumToStringConverter<GenderEnum>();
            builder.Property(p => p.Gender).HasMaxLength(20).HasConversion(converter);
        }
    }
}
