using CvViewer.ApplicationServices;
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
                options.UseSqlServer(connectionString, options => options.UseNodaTime());
            }
            else
            {
                options.UseInMemoryDatabase(InMemoryDatabaseName);
            }
        });

        services.AddScoped<ICvRepository, CvRepository>();

        return services;
    }
}
