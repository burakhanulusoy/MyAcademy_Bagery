using Bagery.WebUI.MediatorPattern.Results.UserResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.UserQueries;

// Search: ad ya da e-posta, Role: "Admin" / "Writer" / "Waiter" / "User" / "none" (rolsüz)
public record GetAdminUsersQuery(string? Search = null, string? Role = null) : IRequest<GetAdminUsersQueryResult>;