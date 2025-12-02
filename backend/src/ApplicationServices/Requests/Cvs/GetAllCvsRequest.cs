using CvViewer.Domain;
using MediatR;

namespace CvViewer.ApplicationServices.Requests.Cvs;

public sealed record GetAllCvsRequest : IRequest<List<Cv>?>;
