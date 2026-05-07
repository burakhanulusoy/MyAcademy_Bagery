using Bagery.WebUI.MediatorPattern.Queries.TestimonialQueries;
using Bagery.WebUI.MediatorPattern.Results.TestimonialResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.TestimonialHandlers
{
    public class GetTestimonialByIdQueryHandler : IRequestHandler<GetTestimonialByIdQuery, GetTestimonialByIdQueryResult>
    {
        public Task<GetTestimonialByIdQueryResult> Handle(GetTestimonialByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
