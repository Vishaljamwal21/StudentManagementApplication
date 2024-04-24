using Microsoft.AspNetCore.Mvc;
using SMS_APP.Models;
using SMS_APP.Repository;

namespace SMS_APP.Controllers
{
    public class StudentController : Controller
    {
        private readonly Repository<Student> _studentRepository;

        public StudentController(Repository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _studentRepository.GetAllAsync(URL.StudentAPIPath) });
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
            if (ModelState.IsValid)
            {
                if (student.Id == 0)
                    await _studentRepository.CreateAsync(URL.StudentAPIPath, student);
                else
                    await _studentRepository.UpdateAsync(URL.StudentAPIPath, student);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(student);
            }
        }
    }
}
