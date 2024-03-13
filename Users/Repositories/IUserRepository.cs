using Users.DTO;
using Users.Models;

namespace Users.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAll();

        public Task Create(User user);

        public Task<User?> GetUserById(Guid id);

        public Task<User?> GetUserByEmail(string email);
    }
}
