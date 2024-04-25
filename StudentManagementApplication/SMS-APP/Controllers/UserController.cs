using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Register(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _userRepository.Register(userVM.Email, userVM.Password);
                if (response != null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                }
            }

            return View(userVM);
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
                    SessionHandler.SetToken(HttpContext, result.Token);
                    SessionHandler.SetUserEmail(HttpContext, result.Email);
                    SessionHandler.SetUserRole(HttpContext, result.Role);

                    if (SessionHandler.GetToken(HttpContext) != null && SessionHandler.GetUserEmail(HttpContext) != null && SessionHandler.GetUserRole(HttpContext) != null)
                    {
                        SessionHandler.SetToken(HttpContext, result.Token);
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        throw new Exception("Token, email, or role is not stored in session.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest();
            }

            return View(userVM);
        }
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
       
    }
}
