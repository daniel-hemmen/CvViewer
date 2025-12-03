using MediatR;

namespace CvViewer.ApplicationServices.Requests.Cvs;

public sealed record ToggleCvIsFavoritedCommand : IRequest<bool?>
{
    public Guid ExternalId { get; }

    public ToggleCvIsFavoritedCommand(Guid externalId)
    {
        ExternalId = externalId;
    }
}
