using Bagery.WebUI.MediatorPattern.Results.PanelResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.PanelQueries;

// Giriş yapan yazarın kendi istatistikleri
public record GetWriterDashboardQuery : IRequest<GetWriterDashboardQueryResult>;