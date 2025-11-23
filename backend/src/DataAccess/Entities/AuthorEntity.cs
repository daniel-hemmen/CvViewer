namespace CvViewer.DataAccess.Entities
{
    public class AuthorEntity
    {
        public long Id { get; set; }
        public required string FirstName { get; set; }
        public string? Infix { get; set; }
        public required string LastName { get; set; }
        public required AddressEntity Address { get; set; }
        public required ContactInformationEntity ContactInformation { get; set; }
        public List<CvEntity> Cvs { get; set; } = [];
    }
}
