using Users.Models;

namespace Users.Utils
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
