namespace Bagery.WebUI.MediatorPattern.Results.UserResults
{
    public class GetUserRoleQueryResult
    {
        public Guid RoleId { get; set; }
        public Guid Id { get; set; }
        public bool RoleExist { get; set; }
        public string   RoleName { get; set; }
    }
}
