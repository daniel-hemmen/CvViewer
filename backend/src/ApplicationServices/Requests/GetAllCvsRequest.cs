using CvViewer.Domain;
using MediatR;

namespace CvViewer.ApplicationServices.Requests;

public sealed record GetAllCvsRequest : IRequest<List<Cv>?>;
