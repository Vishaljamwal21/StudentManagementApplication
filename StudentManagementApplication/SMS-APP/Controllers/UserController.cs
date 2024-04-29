using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using SMS_APP.Models;
using SMS_APP.Repository.IRepository;

namespace SMS_APP.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userRepository.Register(user.Email, user.Password);
                if (response != null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                }
            }

            return View(user);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userVM);
            }
            try
            {
                var result = await _userRepository.Authenticate(userVM.Email, userVM.Password);
                if (result != null && !string.IsNullOrEmpty(result.Token))
                {
                    // Set server-side session data
                    SessionHandler.SetToken(HttpContext, result.Token);
                    SessionHandler.SetUserEmail(HttpContext, result.Email);
                    SessionHandler.SetUserRole(HttpContext, result.Role);

                    if (SessionHandler.GetUserRole(HttpContext) == "Teacher")
                    {
                        return RedirectToAction("Index", "Enrollment");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Student");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return View(userVM);
        }
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("token");
            return RedirectToAction("Login");
        }
    }
}
