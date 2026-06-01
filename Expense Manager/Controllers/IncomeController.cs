using Expense_Manager.DataAccess.Entities;
using Expense_Manager.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Expense_Manager.Controllers
{
    [Authorize]
    public class IncomeController : Controller
    {
        private readonly IIncomeRepository _incomeRepository;
        public IncomeController(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim ?? "0");
        }
        public async Task<IActionResult> Index()
        {
            int userId = GetCurrentUserId();
            var incomes = await _incomeRepository.GetIncomeByUserIdAsycn(userId);
            return View(incomes);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _incomeRepository.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Where(c => c.Type == "Income"), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Income income)
        {
            income.UserId = GetCurrentUserId();
            ModelState.Remove("User");
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                await _incomeRepository.AddAsync(income);
                await _incomeRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var categories = await _incomeRepository.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Where(c => c.Type == "Income"), "Id", "Name");
            return View(income);
        }
        // GET: /Income/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var income = await _incomeRepository.GetByIdAsync(id);

            if (income == null || income.UserId != GetCurrentUserId())
            {
                return NotFound();
            }

            var categories = await _incomeRepository.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Where(c => c.Type == "Income"), "Id", "Name", income.CategoryId);

            return View(income);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Income income)
        {
            if (id != income.Id) return NotFound();

            income.UserId = GetCurrentUserId();
            ModelState.Remove("User");
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                _incomeRepository.Update(income);
                await _incomeRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categories = await _incomeRepository.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Where(c => c.Type == "Income"), "Id", "Name", income.CategoryId);
            return View(income);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _incomeRepository.DeleteIncomeAsync(id);
            await _incomeRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
