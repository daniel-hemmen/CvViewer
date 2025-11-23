using System;
using System.Collections.Generic;
using System.Text;

namespace CvViewer.Domain
{
    public sealed record JobExperience
    {
        public required string Title { get; init; }
        public required string CompanyName { get; init; }
        public required DateTime StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public string? Description { get; init; }
        public bool IsCurrentJob => EndDate?.CompareTo(DateTime.UtcNow)
    }
}
