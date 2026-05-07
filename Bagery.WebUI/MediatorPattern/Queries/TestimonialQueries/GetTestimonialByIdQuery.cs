using Bagery.WebUI.MediatorPattern.Results.TestimonialResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.TestimonialQueries;

public record GetTestimonialByIdQuery(Guid Id):IRequest<GetTestimonialByIdQueryResult>;

