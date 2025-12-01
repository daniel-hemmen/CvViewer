using CvViewer.ApplicationServices;
using CvViewer.DataAccess.Entities;
using CvViewer.DataAccess.Mappers;
using CvViewer.Domain;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace CvViewer.DataAccess;

public sealed class CvRepository : ICvRepository
{
    private readonly CvContext _cvContext;

    public CvRepository(CvContext cvContext)
    {
        _cvContext = cvContext;
    }

    public async Task<bool> AddCvAsync(Cv cv, CancellationToken cancellationToken)
    {
        await _cvContext.AddAsync(cv.ToEntity(), cancellationToken);

        return await _cvContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> AddCvsAsync(CancellationToken cancellationToken, params Cv[] cvs)
    {
        await _cvContext.AddRangeAsync(cvs.Select(cv => cv.ToEntity()), cancellationToken);

        return await _cvContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<int?> GetFavoritedCvCountAsync(CancellationToken cancellationToken)
        => await _cvContext
            .Set<CvEntity>()
            .CountAsync(cv => cv.IsFavorite, cancellationToken);

    public async Task<int?> GetTotalCvCountAsync(CancellationToken cancellationToken)
        => await _cvContext
            .Set<CvEntity>()
            .CountAsync(cancellationToken);

    public async Task<Cv?> GetLastUpdatedCvAsync(CancellationToken cancellationToken)
    {
        var result = await _cvContext.Set<CvEntity>()
            .OrderByDescending(cv => cv.LastUpdated)
            .Include(c => c.Auteur)
            .FirstOrDefaultAsync(cancellationToken);

        return result?.ToDomain();
    }

    public async Task<bool?> ToggleCvIsFavoritedAsync(Guid externalId, CancellationToken cancellationToken)
    {
        var cvEntity = await _cvContext.Set<CvEntity>()
            .FirstOrDefaultAsync(cv => cv.ExternalId == externalId, cancellationToken);

        if (cvEntity is null)
            return null;

        cvEntity.IsFavorite = !cvEntity.IsFavorite;
        await _cvContext.SaveChangesAsync(cancellationToken);

        return cvEntity.IsFavorite;
    }

    public async Task<List<Cv>?> GetCvsUpdatedSinceAsync(Instant since, CancellationToken cancellationToken)
    {
        var result = await _cvContext.Set<CvEntity>()
            .Where(cv => cv.LastUpdated >= since)
            .Include(c => c.Auteur)
            .ToListAsync(cancellationToken);

        return result?.Select(cv => cv.ToDomain()).ToList();
    }

    public async Task<List<Cv>?> GetAllCvsAsync(CancellationToken cancellationToken)
    {
        var result = await _cvContext.Set<CvEntity>()
            .Include(c => c.Auteur)
            .ToListAsync(cancellationToken);

        return result?.Select(cv => cv.ToDomain()).ToList();
    }

    public async Task<Cv?> GetCvByIdAsync(Guid cvExternalId, CancellationToken cancellationToken)
    {
        var result = await _cvContext.Set<CvEntity>()
            .FirstOrDefaultAsync(cv => cv.ExternalId == cvExternalId, cancellationToken);

        return result?.ToDomain();
    }
}
