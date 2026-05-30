using Expense_Manager.DataAccess.Entities;

namespace Expense_Manager.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task<bool> SaveChangesAsync();  

    }
}
