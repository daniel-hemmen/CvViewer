namespace CvViewer.DataAccess.Entities
{
    public class AddressEntity
    {
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; }
    }
}