using CvViewer.DataAccess;
using CvViewer.Test.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CvViewer.ComponentTests;

public sealed class TestApiFactory : WebApplicationFactory<Api.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
        => builder.ConfigureServices(services =>
        {
            services
                .RemoveAll<DbContextOptions<CvContext>>()
                .RemoveAll<CvContext>()
                .AddSingleton<TestCvContextFactory>()
                .AddScoped(services => services.GetRequiredService<TestCvContextFactory>().CreateContext());
        });
}
