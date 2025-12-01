using CvViewer.ComponentTests.Preparators;
using CvViewer.ComponentTests.Validators;
using Shouldly;

namespace CvViewer.ComponentTests.Steps;

[Binding]
public partial class StatusWijzigenSteps
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;
    private readonly State _state;

    public TestApiClient ApiClient { get; }
    public CvPreparator CvPreparator { get; }
    public CvValidator CvValidator { get; }

    public StatusWijzigenSteps(
        State state,
        TestApiClient apiClient,
        CvPreparator cvPreparator,
        CvValidator cvValidator)
    {
        _state = state;
        ApiClient = apiClient;
        CvPreparator = cvPreparator;
        CvValidator = cvValidator;
    }

    [Given("er is één CV en deze is niet als favoriet aangemerkt")]
    public async Task GivenErIsEenCVEnDezeIsNietAlsFavorietAangemerkt()
        => _state.CvExternalId = await CvPreparator.PrepareCvAsync(_cancellationToken);

    [When("een verzoek wordt ontvangen om deze CV als favoriet aan te merken")]
    public async Task WhenEenVerzoekWordtOntvangenOmDezeCVAlsFavorietAanTeMerken()
        => _state.ToggleCvIsFavoritedResponse = await ApiClient.ToggleCvIsFavoritedAsync(_state.CvExternalId, _cancellationToken);

    [Then("wordt als antwoord gegeven dat het CV als favoriet is aangemerkt")]
    public void ThenWordtAlsAntwoordGegevenDatHetCVAlsFavorietIsAangemerkt()
        => _state.ToggleCvIsFavoritedResponse.ShouldBeTrue();
}
