using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CvViewer.DataAccess;

public class CvContextFactory : IDesignTimeDbContextFactory<CvContext>
{
    public CvContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CvContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=CvDb;Trusted_Connection=True;Encrypt=False;",
            sql =>
            {
                sql.UseNodaTime();
                sql.EnableRetryOnFailure();
            });

        return new CvContext(optionsBuilder.Options);
    }
}
