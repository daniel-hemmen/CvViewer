namespace CvViewer.ApplicationServices.DTOs;

public sealed record BlobDownloadResultDto(Stream Content, string FileName, string ContentType);
