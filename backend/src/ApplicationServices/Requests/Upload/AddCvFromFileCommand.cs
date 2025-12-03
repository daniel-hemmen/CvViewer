using MediatR;

namespace CvViewer.ApplicationServices.Requests.Upload;

public sealed record AddCvFromFileCommand(Stream File) : IRequest<bool>;
