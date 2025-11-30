using System.Text.Json;
using CvViewer.DataAccess.Configurations;
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
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(CvEntityConfiguration).Assembly);
}
