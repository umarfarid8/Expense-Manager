using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Expense_Manager.DataAccess.Entities;
namespace Expense_Manager.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Salary", Type = "Income" },
                new Category { Id = 2, Name = "Freelance", Type = "Income" },
                new Category { Id = 3, Name = "Groceries", Type = "Expense" },
                new Category { Id = 4, Name = "Rent", Type = "Expense" },
                new Category { Id = 5, Name = "Entertainment", Type = "Expense" }
                );
        }
    } 
}
