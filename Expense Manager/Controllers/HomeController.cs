// Controllers/HomeController.cs
using Expense_Manager.DataAccess.Repositories;
using Expense_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Expense_Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;

        public HomeController(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
        }

      

        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return RedirectToAction("Login", "Account");
            }

            var userIdClaim =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim ?? "0");

            var incomes = await _incomeRepository.GetIncomeByUserIdAsycn(userId);
            var expenses = await _expenseRepository.GetExpenseByUserIdAsync(userId);

            var dashboardData = new DashboardViewModel
            {
                TotalIncome = incomes.Sum(i => i.Amount),
                TotalExpense = expenses.Sum(e => e.Amount),
            };
            return View(dashboardData);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}