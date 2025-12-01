using CvViewer.Domain;
using NodaTime;

namespace CvViewer.ApplicationServices;

public interface ICvRepository
{
    Task<bool> AddCvAsync(Cv cv, CancellationToken cancellationToken);
    Task<bool> AddCvsAsync(CancellationToken cancellationToken, params Cv[] cvs);
    public Task<int?> GetFavoritedCvCountAsync(CancellationToken cancellationToken);
    public Task<Cv?> GetLastUpdatedCvAsync(CancellationToken cancellationToken);
    public Task<int?> GetTotalCvCountAsync(CancellationToken cancellationToken);
    public Task<List<Cv>?> GetCvsUpdatedSinceAsync(Instant since, CancellationToken cancellationToken);
    public Task<List<Cv>?> GetAllCvsAsync(CancellationToken cancellationToken);
    public Task<Cv?> GetCvByIdAsync(Guid cvExternalId, CancellationToken cancellationToken);
    public Task<bool?> ToggleCvIsFavoritedAsync(Guid externalId, CancellationToken cancellationToken);
}
