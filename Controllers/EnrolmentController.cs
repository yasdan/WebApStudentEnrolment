using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class EnrolmentController : Controller
    {
        // dependency injection for the enrolment repository

        private readonly IEnrolments _enrolmentRepo;
        private readonly IStudent _student;
        private readonly ICourse _course;

       // public EnrolmentController() { }

        public EnrolmentController(IEnrolments enrolmentRepo, IStudent student, ICourse course  )
        {
            _enrolmentRepo = enrolmentRepo;
            _student = student;
            _course = course;
        }
        // GET: Enrolments
        public async Task<IActionResult> Index()
        {
            var enrolments = await _enrolmentRepo.GetAllEnrolments();


            /*search logic
            var Enrolment_Course = from enr in enrolments
                                                  orderby enr.Course.Name
                                                  select enr;

            if(!string.IsNullOrEmpty(bycourse))
            {
                enrolments= enrolments.Where(x => x.Course.Name == bycourse).ToList();
            }

            var courseenrols = new SelectList(Enrolment_Course.Distinct().ToList());
            var enrolementsbycourse = new ViewModel()
            {
                
                Courselist = courseenrols,    
            };
            */
            return View(enrolments);
            //return View(enrolementsbycourse);
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
        
        public async Task<ViewResult> Create()
        {
            // get the data from student and course tables to select the studentid, CourseId from the 
            // dropdown lists.

            var students = await _student.GetAllStudents();
            ViewBag.StudentId = students.Any()
                ? new SelectList(students, "Id", "Name")
                : new SelectList(new List<SelectListItem>());

            var courses = await _course.GetAllCourses();
            ViewBag.CourseId = courses.Any()
                ? new SelectList(courses, "Id", "Name")
                : new SelectList(new List<SelectListItem>());

            return View();


        }
        // POST: Enrolments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrolment)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrolment)
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
