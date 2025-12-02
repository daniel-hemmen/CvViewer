using CvViewer.ApplicationServices.Handlers.Upload;
using MediatR;

namespace CvViewer.ApplicationServices.Requests.Upload
{
    public sealed class GetTemplateRequest : IRequest<DownloadResult?>;
}
