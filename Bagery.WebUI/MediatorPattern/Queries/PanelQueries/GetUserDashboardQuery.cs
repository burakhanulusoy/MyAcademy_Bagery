using Bagery.WebUI.MediatorPattern.Results.PanelResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.PanelQueries;

// Giriş yapan üyenin kendi istatistikleri
public record GetUserDashboardQuery : IRequest<GetUserDashboardQueryResult>;