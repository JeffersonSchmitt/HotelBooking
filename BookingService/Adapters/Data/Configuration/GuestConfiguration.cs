using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Document)
                .Property(x => x.IdNumber);

            builder.OwnsOne(x => x.Document)
                .Property(x => x.DocumentType);
        }
    }
}
