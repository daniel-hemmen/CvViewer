namespace CvViewer.ApplicationServices.Exceptions;

public sealed class CvCreationException : Exception
{
    public CvCreationException()
        : base("Failed to create CV.")
    {
    }
}
