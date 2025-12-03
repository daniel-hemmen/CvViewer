using CvViewer.ApplicationServices.FileReader;
using CvViewer.ApplicationServices.Requests.Upload;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers.Upload;

public sealed class AddCvFromFileCommandHandler : IRequestHandler<AddCvFromFileCommand, bool>
{
    private readonly ICvFileReader _cvFileReader;
    private readonly ICvRepository _cvRepository;

    public AddCvFromFileCommandHandler(ICvFileReader cvFileReader, ICvRepository cvRepository)
    {
        _cvFileReader = cvFileReader;
        _cvRepository = cvRepository;
    }

    public async Task<bool> Handle(AddCvFromFileCommand request, CancellationToken cancellationToken)
    {
        var cv = _cvFileReader.ReadCvFromFile(request.File);

        return await _cvRepository.AddCvAsync(cv, cancellationToken);
    }
}
