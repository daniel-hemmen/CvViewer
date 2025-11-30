namespace CvViewer.DataAccess.Exceptions;

public sealed class ResourceNotFoundException(string message) : Exception(message)
{
}
