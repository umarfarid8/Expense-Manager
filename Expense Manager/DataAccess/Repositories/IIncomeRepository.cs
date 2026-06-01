using Expense_Manager.DataAccess.Entities;

namespace Expense_Manager.DataAccess.Repositories
{
    public interface IIncomeRepository
    {
        Task<IEnumerable<Income>> GetIncomeByUserIdAsycn(int userId);
        Task<Income?> GetByIdAsync(int id);
        Task AddAsync(Income income);
        void Update(Income income);
        Task DeleteIncomeAsync(int Id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<bool> SaveChangesAsync();

    }
}
