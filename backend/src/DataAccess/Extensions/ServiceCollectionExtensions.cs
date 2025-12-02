using CvViewer.ApplicationServices;
using CvViewer.DataAccess.Entities;
using CvViewer.DataAccess.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CvViewer.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    private const string InMemoryDatabaseName = "CvViewerInMemoryDb";

    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<CvContext>(options =>
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                options.UseSqlServer(connectionString, options =>
                {
                    options.UseNodaTime();
                    options.EnableRetryOnFailure();
                })
                .UseSeeding((context, _) =>
                {
                    var cvs = context.Set<CvEntity>().FirstOrDefault();
                    if (cvs == null)
                    {
                        DatabaseSeeder.Seed((CvContext)context);
                    }
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    var cvs = await context.Set<CvEntity>().FirstOrDefaultAsync(cancellationToken);

                    if (cvs == null)
                    {
                        await DatabaseSeeder.SeedAsync((CvContext)context, cancellationToken);
                    }
                });
            }
            else
            {
                options.UseInMemoryDatabase(InMemoryDatabaseName)
                .UseSeeding((context, _) =>
                {
                    var cvs = context.Set<CvEntity>().FirstOrDefault();
                    if (cvs == null)
                    {
                        DatabaseSeeder.Seed((CvContext)context);
                    }
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    var cvs = await context.Set<CvEntity>().FirstOrDefaultAsync(cancellationToken);

                    if (cvs == null)
                    {
                        await DatabaseSeeder.SeedAsync((CvContext)context, cancellationToken);
                    }
                });
            }
        });

        services.AddScoped<ICvRepository, CvRepository>();

        return services;
    }
}
