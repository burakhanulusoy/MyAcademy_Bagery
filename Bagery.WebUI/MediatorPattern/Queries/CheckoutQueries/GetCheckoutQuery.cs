using Bagery.WebUI.MediatorPattern.Results.CheckoutResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.CheckoutQueries;

public record GetCheckoutQuery : IRequest<GetCheckoutQueryResult>;