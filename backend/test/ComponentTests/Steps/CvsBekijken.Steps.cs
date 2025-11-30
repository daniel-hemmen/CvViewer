using CvViewer.ComponentTests.Preparators;
using Shouldly;

namespace CvViewer.ComponentTests.Steps;

[Binding]
public partial class CvsOpvragenSteps
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;
    private readonly State _state;

    public TestApiClient ApiClient { get; }
    public CvPreparator CvPreparator { get; }

    public CvsOpvragenSteps(
        State state,
        TestApiClient apiClient,
        CvPreparator cvPreparator)
    {
        _state = state;
        ApiClient = apiClient;
        CvPreparator = cvPreparator;
    }

    [Given("er zijn {int} CVs waarvan er {int} als favoriet zijn aangemerkt")]
    public async Task GivenErZijnCvsWaarvanErAlsFavorietZijnAangemerkt(int totalCvCount, int favoritedCount)
    {
        var notFavoritedCount = totalCvCount - favoritedCount;

        await CvPreparator.PrepareCvsAsync(notFavoritedCount, _cancellationToken);
        await CvPreparator.PrepareFavoritedCvsAsync(favoritedCount, _cancellationToken);
    }

    [When("het aantal als favoriet aangemerkte CVs wordt opgevraagd")]
    public async Task WhenHetAantalAlsFavorietAangemerkteCVsWordtOpgevraagd()
        => _state.GetFavoritedCvCountResponse = await ApiClient.GetFavoritedCountAsync(_cancellationToken);

    [Then("wordt als antwoord gegeven dat er {int} als favoriet aangemerkte CVs zijn")]
    public void ThenWordtAlsAntwoordGegevenDatErAlsFavorietAangemerkteCVsZijn(int expectedFavoritedCount)
        => _state.GetFavoritedCvCountResponse.ShouldBe(expectedFavoritedCount);
}
