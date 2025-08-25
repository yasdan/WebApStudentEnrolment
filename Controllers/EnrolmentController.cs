using Microsoft.AspNetCore.Mvc;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class EnrolmentController : Controller
    {
        // dependency injection for the enrolment repository

        private readonly IEnrolments _enrolmentRepo;
       // public EnrolmentController() { }

        public EnrolmentController(IEnrolments enrolmentRepo)
        {
            _enrolmentRepo = enrolmentRepo;
        }
        // GET: Enrolments
        public async Task<IActionResult> Index()
        {
            var enrolments = await _enrolmentRepo.GetAllEnrolments();
            return View(enrolments);
        }

        // GET: Enrolments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var enrolment = await _enrolmentRepo.GetEnrolmentById(id);
            if (enrolment == null)
            {
                return NotFound();
            }
            return View(enrolment);
        }
        // GET: Enrolments/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Enrolments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,EnrolmentDate")] WebApStudentEnrolment.Models.Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                await _enrolmentRepo.AddEnrolment(enrolment);
                return RedirectToAction(nameof(Index));
            }
            return View(enrolment);
        }

        // GET: Enrolments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var enrolment = await _enrolmentRepo.GetEnrolmentById(id);
            if (enrolment == null)
            {
                return NotFound();
            }
            return View(enrolment);
        }
        // POST: Enrolments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,EnrolmentDate")] WebApStudentEnrolment.Models.Enrolment enrolment)
        {
            if (id != enrolment.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _enrolmentRepo.UpdateEnrolment(id, enrolment);
                return RedirectToAction(nameof(Index));
            }
            return View(enrolment);
        }

        // GET: Enrolments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var enrolment = await _enrolmentRepo.GetEnrolmentById(id);
            if (enrolment == null)
            {
                return NotFound();
            }
            return View(enrolment);
        }

        // POST: Enrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _enrolmentRepo.DeleteEnrolment(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
