using CvViewer.ApplicationServices;
using CvViewer.DataAccess.Entities;
using CvViewer.DataAccess.Mappers;
using CvViewer.Domain;
using Microsoft.EntityFrameworkCore;

namespace CvViewer.DataAccess
{
    public sealed class CvRepository : ICvRepository
    {
        private readonly CvContext _cvContext;

        public CvRepository(CvContext cvContext)
        {
            _cvContext = cvContext;
        }

        public async Task<bool> CreateCvAsync(Cv cv, CancellationToken cancellationToken)
        {
            await _cvContext.AddAsync(cv.ToEntity(), cancellationToken);

            return await _cvContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<int> GetFavoritedCvCountAsync(CancellationToken cancellationToken)
            => await _cvContext.Set<CvEntity>().CountAsync(cv => cv.IsFavorite, cancellationToken);
    }
}
