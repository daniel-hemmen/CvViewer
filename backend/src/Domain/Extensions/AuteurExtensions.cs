using CvViewer.Domain;
using CvViewer.Domain.Extensions;

namespace CvViewer.Domain.Extensions;

public static class AuteurExtensions
{
    extension(Auteur auteur)
    {
        public string FullName
        => string.Join(" ", auteur.GetNameParts().Where(namePart => !string.IsNullOrWhiteSpace(namePart)));

        private string?[] GetNameParts()
            => [auteur.Voornaam, auteur.Tussenvoegsel, auteur.Achternaam];
    }
}
