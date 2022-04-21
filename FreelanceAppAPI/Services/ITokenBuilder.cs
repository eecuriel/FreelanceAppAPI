using FreelanceAppAPI.Models;

namespace FreelanceAppAPI.Services
{
    public interface ITokenBuilder
    {
        UserToken BuildToken(UserAccountModel userData, IList<string> roles, string userId);
    }
}