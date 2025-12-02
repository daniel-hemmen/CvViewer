using CvViewer.ApplicationServices.Extensions;
using CvViewer.DataAccess;
using CvViewer.DataAccess.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

namespace CvViewer.Api;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(
                    "http://localhost:4200",
                    "https://localhost:4200",
                    "https://zealous-sand-011043103.3.azurestaticapps.net"
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING")
            ?? builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services
            .AddApplicationServices()
            .AddDataAccessServices(connectionString);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            ApplyMigrations(app);
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseHealthChecks("/health");

        app.UseCors("AllowFrontend");

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void ApplyMigrations(WebApplication? app)
    {
        using var scope = app!.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        context.Database.Migrate();
    }
}
