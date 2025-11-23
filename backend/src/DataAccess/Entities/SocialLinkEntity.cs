namespace CvViewer.DataAccess.Entities
{
    public class SocialLinkEntity
    {
        public long Id { get; set; }
        public required string Platform { get; set; }
        public required string Url { get; set; }
    }
}
