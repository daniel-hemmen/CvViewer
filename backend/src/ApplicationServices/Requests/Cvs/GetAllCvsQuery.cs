using CvViewer.Domain;
using MediatR;

namespace CvViewer.ApplicationServices.Requests.Cvs;

public sealed record GetAllCvsQuery : IRequest<List<Cv>?>;
