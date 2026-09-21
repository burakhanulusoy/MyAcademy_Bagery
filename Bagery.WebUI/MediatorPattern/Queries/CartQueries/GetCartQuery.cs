using Bagery.WebUI.Models.CartModels;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.CartQueries;

// Sepeti sadece okur, değiştirmez. Bu yüzden Command değil Query.
public record GetCartQuery : IRequest<Cart>;