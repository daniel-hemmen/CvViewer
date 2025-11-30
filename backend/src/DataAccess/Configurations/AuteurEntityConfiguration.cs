using CvViewer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CvViewer.DataAccess.Configurations;

public class AuteurEntityConfiguration : IEntityTypeConfiguration<AuteurEntity>
{
    public void Configure(EntityTypeBuilder<AuteurEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasMany(a => a.Cvs)
            .WithOne(c => c.Auteur)
            .HasForeignKey(c => c.AuteurId)
            .IsRequired();

        builder.OwnsOne(a => a.Contactgegevens, ci =>
        {
            ci.Property(p => p.Email).HasMaxLength(256);
            ci.Property(p => p.Telefoonnummer).HasMaxLength(50);
            ci.Property(p => p.LinkedInUrl).HasMaxLength(256);
        });

        builder.OwnsOne(a => a.Adres, ad =>
        {
            ad.Property(p => p.Straat).HasMaxLength(200);
            ad.Property(p => p.Postcode).HasMaxLength(20);
            ad.Property(p => p.Plaats).HasMaxLength(100);
            ad.Property(p => p.Land).HasMaxLength(100);
        });
    }
}
