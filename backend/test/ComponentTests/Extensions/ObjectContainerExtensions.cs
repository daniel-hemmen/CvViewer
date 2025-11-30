using CvViewer.ApplicationServices;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.BoDi;

namespace CvViewer.ComponentTests.Extensions;

public static class ObjectContainerExtensions
{
    public static IObjectContainer RegisterTestApiFactory(this IObjectContainer container)
    {
        container.RegisterInstanceAs<WebApplicationFactory<Api.Program>>(new TestApiFactory());
        container.RegisterFactoryAs(container =>
        {
            var testApiFactory = container.Resolve<WebApplicationFactory<Api.Program>>();

            return testApiFactory.CreateClient();
        });

        return container;
    }

    public static IObjectContainer RegisterScopedServiceProvider(this IObjectContainer container)
    {
        container.RegisterFactoryAs(container => container.Resolve<WebApplicationFactory<Api.Program>>().Services.CreateScope());
        container.RegisterFactoryAs(container => container.Resolve<IServiceScope>().ServiceProvider);

        return container;
    }

    public static IObjectContainer RegisterCvRepository(this IObjectContainer container)
    {
        container.RegisterFactoryAs(c =>
        {
            var serviceProvider = c.Resolve<IServiceProvider>();
            return serviceProvider.GetRequiredService<ICvRepository>();
        });

        return container;
    }

    public static IObjectContainer DisposeServiceScope(this IObjectContainer container)
    {
        var scope = container.Resolve<IServiceScope>();
        scope.Dispose();

        return container;
    }

    public static IObjectContainer DisposeTestApiFactory(this IObjectContainer container)
    {
        var testApiFactory = container.Resolve<TestApiFactory>();
        testApiFactory.Dispose();

        return container;
    }
}
