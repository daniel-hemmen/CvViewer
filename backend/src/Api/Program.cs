using CvViewer.ApplicationServices.Extensions;
using CvViewer.DataAccess;
using CvViewer.DataAccess.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace CvViewer.Api;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING")!;

        builder.Services
            .AddApplicationServices()
            .AddDataAccessServices(connectionString);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            EnsureDatabaseCreated(app);
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void EnsureDatabaseCreated(WebApplication? app)
    {
        using var scope = app!.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        context.Database.EnsureCreated();
    }
}
