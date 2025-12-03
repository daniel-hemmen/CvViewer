using CvViewer.Domain;

namespace CvViewer.ApplicationServices.FileReader;

public interface ICvFileReader
{
    public Cv ReadCvFromFile(Stream fileStream);
}
