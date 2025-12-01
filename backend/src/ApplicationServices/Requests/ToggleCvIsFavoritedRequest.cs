using MediatR;

namespace CvViewer.ApplicationServices.Requests;

public sealed record ToggleCvIsFavoritedRequest : IRequest<bool?>
{
    public Guid ExternalId { get; }

    public ToggleCvIsFavoritedRequest(Guid externalId)
    {
        ExternalId = externalId;
    }
}
