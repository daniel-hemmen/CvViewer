using CvViewer.Domain;
using MediatR;
using NodaTime;

namespace CvViewer.ApplicationServices.Requests;

public sealed record GetCvsUpdatedSinceRequest(Instant Since) : IRequest<List<Cv>?>;
