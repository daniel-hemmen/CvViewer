namespace CvViewer.DataAccess.Entities
{
    public class CertificateEntity
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string IssuingOrganization { get; set; }
        public required DateOnly IssueDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public string? CredentialUrl { get; set; }
    }
}
