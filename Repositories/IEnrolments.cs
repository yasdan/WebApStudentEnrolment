using Microsoft.AspNetCore.Mvc;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public interface IEnrolments
    {
         int Count { get; }
         
        public Course? Course { get; set; } 
        public Student? Student { get; set; }
      //  public IEnumerable<Enrolment> Enrolments { get; }
        
        
        Task AddEnrolment(Enrolment enrolment);
        Task<Enrolment> GetEnrolmentById(int enrolmentId);
        Task<IEnumerable<Enrolment>> GetAllEnrolments();
        Task UpdateEnrolment(int enrolmentId, Enrolment enrolment);
        Task DeleteEnrolment(int enrolmentId);



    }
}
