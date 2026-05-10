using Bagery.WebUI.MediatorPattern.Queries.TestimonialQueries;
using Bagery.WebUI.MediatorPattern.Results.TestimonialResults;
using Bagery.WebUI.Repositories.TestimonialRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.TestimonialHandlers
{
    public class GetTestimonialByIdQueryHandler(ITestimonialRepository _testimonialRepository) : IRequestHandler<GetTestimonialByIdQuery, GetTestimonialByIdQueryResult>
    {
        public async Task<GetTestimonialByIdQueryResult> Handle(GetTestimonialByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _testimonialRepository.GetByIdAsync(request.Id);
            return item.Adapt<GetTestimonialByIdQueryResult>();
            

        }
    }
}
