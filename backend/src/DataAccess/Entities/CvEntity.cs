namespace CvViewer.DataAccess.Entities
{
    public class CvEntity
    {
        public int Id { get; set; }
        public long AuthorId { get; set; }
        public required AuthorEntity Author { get; set; }
    }
}
