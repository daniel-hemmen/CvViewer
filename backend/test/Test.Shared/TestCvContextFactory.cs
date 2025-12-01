using CvViewer.DataAccess;
using CvViewer.DataAccess.Entities;
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

        using var context = new TestCvContext(_options);
        context.Database.EnsureCreated();
    }

    public CvContext CreateContext() => new TestCvContext(_options);

    private class TestCvContext : CvContext
    {
        public TestCvContext(DbContextOptions<CvContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CvEntity>().Ignore(x => x.RowVersion);
        }
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _connection.Dispose();
        _disposed = true;

        GC.SuppressFinalize(this);
    }
}
