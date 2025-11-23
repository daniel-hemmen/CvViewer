namespace CvViewer.DataAccess.Entities
{
    public class EducationEntity
    {
        public long Id { get; set; }
        public required string Title { get; set; }
        public required string InstitutionName { get; set; }
        public required DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
    }
}
