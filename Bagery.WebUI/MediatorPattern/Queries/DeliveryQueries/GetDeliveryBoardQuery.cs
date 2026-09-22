using Bagery.WebUI.MediatorPattern.Results.DeliveryResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.DeliveryQueries;

public record GetDeliveryBoardQuery : IRequest<GetDeliveryBoardQueryResult>;