using Bagery.WebUI.MediatorPattern.Results.TestimonialResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.TestimonialQueries;

public record GetTestimonialsQuery:IRequest<List<GetTestimonialsQueryResult>>;
