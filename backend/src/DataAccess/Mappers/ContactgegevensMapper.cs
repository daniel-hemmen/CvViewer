using CvViewer.DataAccess.Entities;

namespace CvViewer.DataAccess.Mappers
{
    public static class ContactgegevensMapper
    {
        public static ContactgegevensEntity ToEntity(this Domain.Contactgegevens? contactgegevens)
        {
            ArgumentNullException.ThrowIfNull(contactgegevens);

            return new()
            {
                Email = contactgegevens.Email,
                Telefoonnummer = contactgegevens.Telefoonnummer,
                LinkedInUrl = contactgegevens.LinkedInUrl
            };
        }
    }
}
