using Microsoft.AspNetCore.Identity;

namespace NEWZEAL_LAND_WORK_API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser identityUser, List<string> roles);
    }
}
 