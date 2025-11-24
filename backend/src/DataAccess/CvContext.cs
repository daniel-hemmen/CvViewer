using System.Text.Json;
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

    public DbSet<AuteurEntity> Authors { get; set; }
    public DbSet<CvEntity> Cvs { get; set; }
    public DbSet<WerkervaringInstanceEntity> JobExperiences { get; set; }
    public DbSet<OpleidingInstanceEntity> Educations { get; set; }
    public DbSet<CertificaatInstanceEntity> Certificates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(CvContext).Assembly);
}
