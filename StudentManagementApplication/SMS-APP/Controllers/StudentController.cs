using Microsoft.AspNetCore.Mvc;
using SMS_APP.Models;
using SMS_APP.Repository;
using SMS_APP.Repository.IRepository;

namespace SMS_APP.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userRole = SessionHandler.GetUserRole(HttpContext);
            if (userRole == "Student")
            {
                var userEmail = SessionHandler.GetUserEmail(HttpContext);
                var studentData = await _studentRepository.GetStudentByEmailAsync(userEmail, URL.StudentAPIPath);
                return Json(new { data = studentData });
            }
            else if (userRole == "Admin" || userRole == "Teacher")
            {
                var allData = await _studentRepository.GetAllAsync(URL.StudentAPIPath);
                return Json(new { data = allData });
            }
            else
            {
                return BadRequest("Unauthorized access");
            }
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepository.GetAsync(URL.StudentAPIPath, id);
            if (student == null)
                return Json(new { success = false, message = "Unable to delete the data" });

            await _studentRepository.DeleteAsync(URL.StudentAPIPath, id);
            return Json(new { success = true, message = "Data Deleted Successfully" });
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SaveorUpdate(int? id)
        {
            Student student = new Student();
            if (id == null)
                return View(student);

            student = await _studentRepository.GetAsync(URL.StudentAPIPath, id.GetValueOrDefault());
            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveorUpdate(Student student)
        {
            var loggedInUserEmail = SessionHandler.GetUserEmail(HttpContext);
            if (student.Email != loggedInUserEmail)
            {
                ModelState.AddModelError(nameof(Student.Email), "The entered email does not match your login email.");
                return View(student);
            }

            if (ModelState.IsValid)
            {
                if (student.Id == 0)
                {
                    // Check if the email already exists in the database
                    var isUniqueEmail = await _studentRepository.IsUniqueEmail(student.Email);
                    if (isUniqueEmail)
                    {
                        ModelState.AddModelError(nameof(Student.Email), "A student with the same email already exists.");
                        return View(student);
                    }

                    await _studentRepository.CreateAsync(URL.StudentAPIPath, student);
                }
                else
                {
                    await _studentRepository.UpdateAsync(URL.StudentAPIPath, student);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(student);
            }
        }


    }
}
