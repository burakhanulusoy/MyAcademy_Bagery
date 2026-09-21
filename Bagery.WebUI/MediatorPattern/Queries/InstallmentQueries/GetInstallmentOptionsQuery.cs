using Bagery.WebUI.MediatorPattern.Results.InstallmentResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Queries.InstallmentQueries;

// Sadece kartın ilk 8 hanesi gelir (PAY_TR'deki InstallmentGetTabloModel.BinNumber)
public record GetInstallmentOptionsQuery(string BinNumber) : IRequest<List<GetInstallmentOptionsQueryResult>>;