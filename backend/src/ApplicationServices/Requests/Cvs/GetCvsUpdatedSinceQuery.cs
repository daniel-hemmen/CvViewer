using CvViewer.Domain;
using MediatR;
using NodaTime;

namespace CvViewer.ApplicationServices.Requests.Cvs;

public sealed record GetCvsUpdatedSinceQuery(Instant Since) : IRequest<List<Cv>?>;
