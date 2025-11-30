using CvViewer.ApplicationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CvViewer.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    private const string InMemoryDatabaseName = "CvViewerDb";

    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddDbContext<CvContext>(options =>
        {
            options.UseInMemoryDatabase(InMemoryDatabaseName);
        });

        services.AddScoped<ICvRepository, CvRepository>();

        return services;
    }
}
