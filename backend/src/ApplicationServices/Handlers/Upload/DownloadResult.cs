namespace CvViewer.ApplicationServices.Handlers.Upload
{
    public sealed record DownloadResult(Stream Content, string FileName, string ContentType);
}
