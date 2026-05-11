using Bagery.WebUI.MediatorPattern.Results.UserResults;

namespace Bagery.WebUI.MediatorPattern.Results.RoleResults
{
    public class GetUserByRolIdQueryResult
    {
        public Guid RoleId { get; set; }
        public GetUserForFobiaTemplateQueryResult Users { get; set; }



    }
}
