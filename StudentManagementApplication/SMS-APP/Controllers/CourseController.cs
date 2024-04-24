using Microsoft.AspNetCore.Mvc;
using SMS_APP.Models;
using SMS_APP.Repository.IRepository;

namespace SMS_APP.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;      
        }

        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _courseRepository.GetAllAsync(URL.CourseAPIPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetAsync(URL.CourseAPIPath, id);
            if (course == null)
                return Json(new { success = false, message = "Unable to delete the data" });

            await _courseRepository.DeleteAsync(URL.CourseAPIPath, id);
            return Json(new { success = true, message = "Data Deleted Successfully" });
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SaveOrUpdate(int? id)
        {
            Course course = new Course();
            if (id != null)
            {
                course = await _courseRepository.GetAsync(URL.CourseAPIPath, id.Value);
                if (course == null)
                    return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveOrUpdate(Course course)
        {
            if (ModelState.IsValid)
            {
                if (course.Id == 0)
                    await _courseRepository.CreateAsync(URL.CourseAPIPath, course);
                else
                    await _courseRepository.UpdateAsync(URL.CourseAPIPath, course);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(course);
            }
        }
    }
}
