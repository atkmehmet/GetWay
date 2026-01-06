using Getway.Interface;

namespace Getway.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        Task IUserRepository.AddUser(int user)
        {
            throw new NotImplementedException();
        }

        Task<List<int>> IUserRepository.ints()
        {
            throw new NotImplementedException();
        }
    }
}
