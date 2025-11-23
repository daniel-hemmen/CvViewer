using CvViewer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CvViewer.DataAccess
{
    public class CvContext(DbContextOptions<CvContext> options) : DbContext(options)
    {
        public DbSet<CvEntity> Cvs { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(CvContext).Assembly);
    }
}
