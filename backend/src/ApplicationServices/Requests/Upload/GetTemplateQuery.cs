using CvViewer.ApplicationServices.DTOs;
using MediatR;

namespace CvViewer.ApplicationServices.Requests.Upload;

public sealed record GetTemplateQuery : IRequest<BlobDownloadResultDto?>;
