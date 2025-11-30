using CvViewer.ApplicationServices.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace CvViewer.ApplicationServices.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetFavoritedCvCountRequestHandler).Assembly));

        return services;
    }
}
