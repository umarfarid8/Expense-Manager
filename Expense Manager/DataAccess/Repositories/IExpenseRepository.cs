using Expense_Manager.DataAccess.Entities;

namespace Expense_Manager.DataAccess.Repositories
{
    public interface IExpenseRepository
    {
            Task<IEnumerable<Expense>> GetExpenseByUserIdAsync(int userId);
            Task<Expense?> GetByIdAsync(int id);
            Task AddAsync(Expense expense);
            void Update(Expense expense);
        void Delete(Expense expense);
        Task DeleteExpenseAsync(int id);

        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
