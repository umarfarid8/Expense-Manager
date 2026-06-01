using Expense_Manager.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Expense_Manager.DataAccess.Entities;
using Expense_Manager.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Expense_Manager.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim ?? "0");
        }
        public async Task<IActionResult> Index()
        {
            int userid = GetCurrentUserId();
            var expenses = await _expenseRepository.GetExpenseByUserIdAsync(userid);
            return View(expenses);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _expenseRepository.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Where(c => c.Type == "Expense"), "Id", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            expense.UserId = GetCurrentUserId();
            ModelState.Remove("User");
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                await _expenseRepository.AddAsync(expense);
                await _expenseRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var categories = await _expenseRepository.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Where(c => c.Type == "Expense"), "Id", "Name");
            return View(expense);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense == null || expense.UserId != GetCurrentUserId())
            {
                return NotFound();
            }
            var categories = await _expenseRepository.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Where(c => c.Type == "Expense"), "Id", "Name", expense.CategoryId);

            return View(expense);
        }

       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id) return NotFound();

            expense.UserId = GetCurrentUserId();
            ModelState.Remove("User");
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                _expenseRepository.Update(expense); 
                await _expenseRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categories = await _expenseRepository.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Where(c => c.Type == "Expense"), "Id", "Name", expense.CategoryId);
            return View(expense);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _expenseRepository.DeleteExpenseAsync(id);
            await _expenseRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}