using Expense_Manager.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expense_Manager.DataAccess.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDbContext _context;
        public IncomeRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Income>> GetIncomeByUserIdAsycn(int userId)
        {
            return await _context.Incomes
                .Include(i => i.Category)
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }
        public async Task<Income?> GetByIdAsync(int id)
        {
            return await _context.Incomes
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task AddAsync(Income income)
        {
            await _context.Incomes.AddAsync(income);

        }
        public void Update(Income income)
        {
            _context.Incomes.Update(income);

        }
        public async Task DeleteIncomeAsync(int Id)
        {
            var income = await _context.Incomes.FindAsync(Id);
            if (income != null)
            {
                _context.Incomes.Remove(income);

            }
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
