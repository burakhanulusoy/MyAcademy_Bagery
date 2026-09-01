namespace Bagery.WebUI.MediatorPattern.Results.ClientResults;

public record GetClientByIdQueryResult(Guid Id, string ClientImageUrl, string Name);

