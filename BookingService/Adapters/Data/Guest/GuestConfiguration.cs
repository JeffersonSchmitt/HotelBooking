using Domain.Guest.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Guest
{
    public class GuestConfiguration : IEntityTypeConfiguration<GuestEntity>
    {
        public void Configure(EntityTypeBuilder<GuestEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Document, document =>
            {
                document.Property(x => x.IdNumber);
                document.Property(x => x.DocumentType);
            });
            //builder.HasKey(x => x.Id);
            //builder.OwnsOne(x => x.Document)
            //    .Property(x => x.IdNumber);

            //builder.OwnsOne(x => x.Document)
            //    .Property(x => x.DocumentType);
        }
    }
}
