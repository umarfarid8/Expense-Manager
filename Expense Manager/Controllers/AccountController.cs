using Expense_Manager.DataAccess.Entities;
using Expense_Manager.DataAccess.Repositories;
using Expense_Manager.Models;
using Expense_Manager.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Expense_Manager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    
    [HttpGet]
        public IActionResult Register()
        {

            if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Home");
            
                
                return View();

         
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var existingUser = await _userRepository.GetByUsernameAsync(model.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("UserName", "Username is already taken");
                return View(model);
            }
            var existingEmail = await _userRepository.GetByEmailAsync(model.EmailAddress);
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "Email is already registered");
                return View(model);

            }
            var user = new User
            {
                UserName = model.UserName,
                Email = model.EmailAddress,
                PasswordHash = PasswordHasher.HashPassword(model.Password)
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            await SignInUserAsync(user);
            return RedirectToAction("Index", "Home");

        }
        
        
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true) 
                return RedirectToAction("Index", "Home");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userRepository.GetByUsernameAsync(model.UserName);
            if (user == null || !PasswordHasher.VerifyPassword(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(model);
            }
            await SignInUserAsync(user);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        private async Task SignInUserAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    } 
}