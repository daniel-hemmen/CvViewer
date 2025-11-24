using System.Text.Json;
using CvViewer.DataAccess.Entities;
using CvViewer.DataAccess.Snapshots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        builder.HasMany(c => c.WerkervaringInstances).WithOne().HasForeignKey("CvId");
        builder.HasMany(c => c.OpleidingInstances).WithOne().HasForeignKey("CvId");
        builder.HasMany(c => c.CertificaatInstances).WithOne().HasForeignKey("CvId").;

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        PropertyBuilder<List<VaardigheidInstanceSnapshot>> vaardigheidProperty = builder.Property(c => c.VaardigheidInstances)
            .HasConversion(
                v => JsonSerializer.Serialize(v, serializerOptions),
                v => JsonSerializer.Deserialize<List<VaardigheidInstanceSnapshot>>(v, serializerOptions) ?? new());

        var valueComparer = new ValueComparer<List<VaardigheidInstanceSnapshot>>(
            equalsExpression: (l1, l2) => JsonSerializer.Serialize(l1, serializerOptions) == JsonSerializer.Serialize(l2, serializerOptions),
            hashCodeExpression: l => l == null ? 0 : JsonSerializer.Serialize(l, serializerOptions).GetHashCode(),
            snapshotExpression: l => JsonSerializer.Deserialize<List<VaardigheidInstanceSnapshot>>(JsonSerializer.Serialize(l, serializerOptions), serializerOptions) ?? new()
        );

        vaardigheidProperty.Metadata.SetValueComparer(valueComparer);

        builder.Property(c => c.Inleiding).HasMaxLength(2000);
    }
}
