using CvViewer.ApplicationServices.Builder;
using CvViewer.ApplicationServices.DTOs;
using CvViewer.ApplicationServices.Exceptions;
using CvViewer.ApplicationServices.Mappers;
using CvViewer.ApplicationServices.Requests;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers
{
    public sealed class DummyHandler : IRequestHandler<DummyRequest, CvDto>
    {
        private readonly ICvRepository _cvRepository;

        public DummyHandler(ICvRepository cvRepository)
        {
            _cvRepository = cvRepository;
        }

        public async Task<CvDto> Handle(DummyRequest request, CancellationToken cancellationToken)
        {
            var cv = CvBuilder.CreateSample();

            if (!await _cvRepository.CreateCvAsync(cv, cancellationToken))
                throw new CvCreationException();

            return cv.ToDto();
        }
    }
}
