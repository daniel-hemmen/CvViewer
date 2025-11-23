namespace CvViewer.DataAccess.Entities
{
    public class JobExperienceEntity
    {
        public long Id { get; set; }
        public required string Title { get; set; }
        public required string CompanyName { get; set; }
        public required DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
    }
}
