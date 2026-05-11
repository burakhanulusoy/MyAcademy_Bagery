namespace Bagery.WebUI.MediatorPattern.Results.RoleResults;

public class GetRolesWithUserCountQueryResult
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public int UserCount { get; set; }
}
