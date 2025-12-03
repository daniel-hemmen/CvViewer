using System.Diagnostics.CodeAnalysis;
using System.Net;
using CvViewer.ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace CvViewer.Api.Validators;

public class CvFileValidator
{
    public static bool TryValidate(IFormFile? file, [NotNullWhen(false)] out ProblemDetails? problemDetails)
    {
        problemDetails = file switch
        {
            null => CreateProblemDetails("File is missing"),
            IFormFile { Length: 0 } => CreateProblemDetails("File is empty"),
            IFormFile { FileName: not Constants.FileName } => CreateProblemDetails($"Name '{file.FileName}' was not expected. Expected: '{Constants.FileName}'"),
            IFormFile { ContentType: not Constants.FileContentType } formFile => CreateProblemDetails($"Content type {formFile.ContentType} was not expected. Expected: '{Constants.FileContentType}'"),

            _ => null
        };

        return problemDetails is null;
    }

    private static ProblemDetails CreateProblemDetails(string detail)
        => new()
        {
            Title = "Error while validating uploaded file",
            Detail = detail,
            Status = (int)HttpStatusCode.BadRequest
        };
}
