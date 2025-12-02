using Azure.Storage.Blobs;
using CvViewer.ApplicationServices.Handlers.Cvs;
using Microsoft.Extensions.DependencyInjection;

namespace CvViewer.ApplicationServices.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, string storageUrl)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetFavoritedCvCountRequestHandler).Assembly));

        services.AddScoped(_ => new BlobServiceClient(new Uri(storageUrl)));

        return services;
    }
}
