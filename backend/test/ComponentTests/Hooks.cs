using CvViewer.ComponentTests.Extensions;
using Reqnroll.BoDi;

namespace CvViewer.ComponentTests;

[Binding]
public sealed class Hooks
{
    [BeforeScenario]
    public static void BeforeScenario(IObjectContainer container)
        => container
            .RegisterTestApiFactory()
            .RegisterScopedServiceProvider()
            .RegisterCvRepository();

    [AfterScenario]
    public static void AfterScenario(IObjectContainer container)
        => container
            .DisposeServiceScope()
            .DisposeTestApiFactory();
}
