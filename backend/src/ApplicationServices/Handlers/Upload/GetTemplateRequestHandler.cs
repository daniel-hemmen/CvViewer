using Azure;
using Azure.Storage.Blobs;
using CvViewer.ApplicationServices.Requests.Upload;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers.Upload
{
    public sealed class GetTemplateRequestHandler : IRequestHandler<GetTemplateRequest, DownloadResult?>
    {
        private readonly BlobServiceClient _blobServiceClient;

        public GetTemplateRequestHandler(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<DownloadResult?> Handle(GetTemplateRequest _, CancellationToken cancellationToken)
        {
            var container = _blobServiceClient.GetBlobContainerClient(Constants.TemplatesContainerName);
            var blob = container.GetBlobClient(Constants.TemplateFileName);

            try
            {
                var downloadResult = await blob.DownloadContentAsync(cancellationToken);

                return new DownloadResult(
                    downloadResult.Value.Content.ToStream(),
                    Constants.TemplateFileName,
                    downloadResult.Value.Details.ContentType ?? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (RequestFailedException)
            {
                return null;
            }
        }
    }
}
