namespace CvViewer.ApplicationServices.DTOs
{
    public class CvDto
    {
        public Guid? Id { get; set; }
        public string? AuteurNaam { get; set; }
        public string? Email { get; set; }
        public string? Locatie { get; set; }
        public string? Inleiding { get; set; }
        public List<string> WerkervaringSamenvattingen { get; set; } = new();
        public List<string> Opleidingen { get; set; } = new();
        public List<string> Certificaten { get; set; } = new();
        public List<string> Vaardigheden { get; set; } = new();
        public bool IsFavorite { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
