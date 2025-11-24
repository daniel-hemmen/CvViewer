using CvViewer.DataAccess.Entities;
using CvViewer.Domain;

namespace CvViewer.DataAccess.Mappers
{
    public static class CvMapper
    {
        public static CvEntity ToEntity(this Cv cv)
            => new()
            {
                Auteur = cv.Auteur.ToEntity(),
                IsFavorite = cv.Metadata is CvMetadata { IsFavorite: true }
            };
    }
}
