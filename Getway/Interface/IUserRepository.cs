namespace Getway.Interface
{
    public interface IUserRepository
    {
        Task AddUser(int user);
        Task<List<int>> ints();
    }
}
