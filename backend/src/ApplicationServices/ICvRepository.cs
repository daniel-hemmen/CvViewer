using CvViewer.Domain;

namespace CvViewer.ApplicationServices
{
    public interface ICvRepository
    {
        Task<bool> CreateCvAsync(Cv cv, CancellationToken cancellationToken);
        public Task<int> GetFavoritedCvCountAsync(CancellationToken cancellationToken);
    }
}
