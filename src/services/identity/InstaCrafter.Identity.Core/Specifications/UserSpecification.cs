using InstaCrafter.Core.Repositories;
using InstaCrafter.Identity.Core.Domain.Entities;

namespace InstaCrafter.Identity.Core.Specifications
{
    public sealed class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string identityId) : base(u => u.IdentityId==identityId)
        {
            AddInclude(u => u.RefreshTokens);
        }
    }
}
