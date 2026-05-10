using Bagery.WebUI.MediatorPattern.Queries.TestimonialQueries;
using Bagery.WebUI.MediatorPattern.Results.TestimonialResults;
using Bagery.WebUI.Repositories.TestimonialRepositories;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.TestimonialHandlers
{
    public class GetTestimonialsQueryHandler(ITestimonialRepository testimonialRepository) : IRequestHandler<GetTestimonialsQuery, List<GetTestimonialsQueryResult>>
    {
        public async Task<List<GetTestimonialsQueryResult>> Handle(GetTestimonialsQuery request, CancellationToken cancellationToken)
        {
            var items = await testimonialRepository.GetAllAsync();
            return items.Adapt<List<GetTestimonialsQueryResult>>();
        }
    }
}
