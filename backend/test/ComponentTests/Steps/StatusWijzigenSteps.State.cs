namespace CvViewer.ComponentTests.Steps;

public partial class StatusWijzigenSteps
{
    [Binding]
    public class State
    {
        public Guid CvExternalId { get; set; }
        public bool IsFavoritedBeforeToggle { get; set; }
        public bool ToggleCvIsFavoritedResponse { get; set; }
    }
}
