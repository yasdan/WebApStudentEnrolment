
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApStudentEnrolment.Models
{
    public class ViewModel
    {
        public List<Enrolment> Enrolments { get; set; }
        public List<Student> students { get; set; }

        public List<Course>  courses { get; set; }

        public  SelectList? Courselist { get; set; }

        public string? EnrolmentByCourse { get; set; }

        public string? SearchString {  get; set; }

       
    }
}
