using CvViewer.DataAccess.Converters;
using CvViewer.DataAccess.Entities;
using CvViewer.DataAccess.Snapshots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CvViewer.DataAccess.Configurations;

public class CvEntityConfiguration : IEntityTypeConfiguration<CvEntity>
{
    public void Configure(EntityTypeBuilder<CvEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Auteur)
            .WithMany(a => a.Cvs)
            .HasForeignKey(c => c.AuteurId)
            .IsRequired();

        builder.OwnsOne(c => c.ContactgegevensSnapshot, cb =>
        {
            cb.Property(p => p.Email).HasMaxLength(256);
            cb.Property(p => p.Telefoonnummer).HasMaxLength(50);
            cb.Property(p => p.LinkedInUrl).HasMaxLength(256);
        });

        builder.OwnsOne(c => c.AdresSnapshot, ab =>
        {
            ab.Property(p => p.Straat).HasMaxLength(200);
            ab.Property(p => p.Postcode).HasMaxLength(20);
            ab.Property(p => p.Stad).HasMaxLength(100);
            ab.Property(p => p.Land).HasMaxLength(100);
        });

        builder.OwnsMany(c => c.WerkervaringInstances, (OwnedNavigationBuilder<CvEntity, WerkervaringInstanceSnapshot> wb) =>
        {
            wb.WithOwner().HasForeignKey("CvId");
            wb.Property<int>("Id");
            wb.HasKey("Id");

            var startProp = wb.Property(w => w.Startdatum).HasConversion(DatePartsConverter.Converter);
            startProp.Metadata.SetValueComparer(DatePartsConverter.ValueComparer);

            var endProp = wb.Property(w => w.Einddatum).HasConversion(DatePartsConverter.NullableConverter);
            endProp.Metadata.SetValueComparer(DatePartsConverter.NullableValueComparer);
        });

        builder.OwnsMany(c => c.OpleidingInstances, (OwnedNavigationBuilder<CvEntity, OpleidingInstanceSnapshot> ob) =>
        {
            ob.WithOwner().HasForeignKey("CvId");
            ob.Property<int>("Id");
            ob.HasKey("Id");

            var startProp = ob.Property(o => o.Startdatum).HasConversion(DatePartsConverter.NullableConverter);
            startProp.Metadata.SetValueComparer(DatePartsConverter.NullableValueComparer);

            var endProp = ob.Property(o => o.Einddatum).HasConversion(DatePartsConverter.Converter);
            endProp.Metadata.SetValueComparer(DatePartsConverter.ValueComparer);
        });

        builder.OwnsMany(c => c.CertificaatInstances, (OwnedNavigationBuilder<CvEntity, CertificaatInstanceSnapshot> cb) =>
        {
            cb.WithOwner().HasForeignKey("CvId");
            cb.Property<int>("Id");
            cb.HasKey("Id");

            var afgifte = cb.Property(c => c.DatumAfgifte).HasConversion(DatePartsConverter.Converter);
            afgifte.Metadata.SetValueComparer(DatePartsConverter.ValueComparer);

            var verloop = cb.Property(c => c.Verloopdatum).HasConversion(DatePartsConverter.NullableConverter);
            verloop.Metadata.SetValueComparer(DatePartsConverter.NullableValueComparer);
        });

        PropertyBuilder<List<VaardigheidInstanceSnapshot>> vaardigheidProperty = builder.Property(c => c.VaardigheidInstances)
            .HasConversion(VaardigheidInstanceConverter.Converter);

        vaardigheidProperty.Metadata.SetValueComparer(VaardigheidInstanceConverter.ValueComparer);

        builder.Property(c => c.Inleiding).HasMaxLength(2000);
    }
}
