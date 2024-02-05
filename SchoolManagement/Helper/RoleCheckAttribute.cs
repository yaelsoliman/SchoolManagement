using Shared.Common;

namespace SchoolManagement.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleCheckAttribute:Attribute
    {
        private readonly Roles[] _roles;

        public RoleCheckAttribute(params Roles[] roles)
        {
            _roles = roles;
        }
    }
}
