using System.Text.Json;
using CvViewer.DataAccess.Configurations;
using CvViewer.DataAccess.Converters;
using CvViewer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CvViewer.DataAccess;

public class CvContext(DbContextOptions<CvContext> options) : DbContext(options)
{
    public static JsonSerializerOptions SerializerOptions => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public DbSet<AuteurEntity> Auteurs { get; set; } = null!;
    public DbSet<CvEntity> Cvs { get; set; } = null!;
    public DbSet<WerkervaringInstanceEntity> WerkervaringInstances { get; set; } = null!;
    public DbSet<OpleidingInstanceEntity> OpleidingInstances { get; set; } = null!;
    public DbSet<CertificaatInstanceEntity> CertificaatInstances { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CvEntityConfiguration).Assembly);

        modelBuilder.Entity<WerkervaringInstanceEntity>(b =>
        {
            b.Property(e => e.Startdatum)
                .HasConversion(DatePartsConverter.Converter)
                .Metadata
                .SetValueComparer(DatePartsConverter.ValueComparer);

            b.Property(e => e.Einddatum)
            .HasConversion(DatePartsConverter.NullableConverter)
            .Metadata
            .SetValueComparer(DatePartsConverter.NullableValueComparer);
        });

        modelBuilder.Entity<OpleidingInstanceEntity>(b =>
        {
            b.Property(e => e.Startdatum)
                .HasConversion(DatePartsConverter.NullableConverter)
                .Metadata
                .SetValueComparer(DatePartsConverter.NullableValueComparer);

            b.Property(e => e.Einddatum)
                .HasConversion(DatePartsConverter.Converter)
                .Metadata
                .SetValueComparer(DatePartsConverter.ValueComparer);
        });

        modelBuilder.Entity<CertificaatInstanceEntity>(b =>
        {
            b.Property(e => e.DatumAfgifte)
                .HasConversion(DatePartsConverter.Converter)
                .Metadata
                .SetValueComparer(DatePartsConverter.ValueComparer);

            b.Property(e => e.Verloopdatum)
                .HasConversion(DatePartsConverter.NullableConverter)
                .Metadata
                .SetValueComparer(DatePartsConverter.NullableValueComparer);
        });
    }
}
