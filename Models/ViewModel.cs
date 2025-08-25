
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApStudentEnrolment.Models
{
    public class ViewModel
    {
        public IEnumerable<Enrolment>? Enrolment { get; set; }
        public IEnumerable<Student>? Student { get; set; }

        public IEnumerable<Course>?  Course { get; set; }

        public  SelectList? Courselist { get; set; }

        public string? EnrolmentByCourse { get; set; }

        public string? SearchString {  get; set; }

       
    }
}
