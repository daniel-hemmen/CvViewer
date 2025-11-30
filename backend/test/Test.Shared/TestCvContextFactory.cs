using CvViewer.DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CvViewer.Test.Shared;

public sealed class TestCvContextFactory : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<CvContext> _options;
    private bool _disposed;

    public TestCvContextFactory()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var builder = new DbContextOptionsBuilder<CvContext>();
        builder.UseSqlite(_connection, options =>
        {
            options.UseNodaTime();
        });
        _options = builder.Options;

        using var context = new CvContext(_options);
        context.Database.EnsureCreated();
    }

    public CvContext CreateContext() => new(_options);

    public void Dispose()
    {
        if (_disposed)
            return;

        _connection.Dispose();
        _disposed = true;

        GC.SuppressFinalize(this);
    }
}
