using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public class EnrolmentRepo : IEnrolments
    {
        public EnrolmentRepo() { }
        private readonly StudentEnrolmentContext _context;
        public EnrolmentRepo(StudentEnrolmentContext context)
        {
            _context = context;
        }

        public int Count { get; private set; }

        public async Task AddEnrolment(Enrolment enrolment)
        {
            // Implementation for adding an enrolment
            await _context.Enrolments.AddAsync(enrolment);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Enrolment> GetEnrolmentById(int enrolmentId)
        {
            // Implementation for retrieving an enrolment by ID
           // var enrolment = await _context.Enrolments.FindAsync(enrolmentId);
           var enrolment = await _context.Enrolments
                .Include(e => e.Student) // Include related Student entity
                .Include(e => e.Course)  // Include related Course entity
                .FirstOrDefaultAsync(e => e.Id == enrolmentId);
            if (enrolment == null)
            {
                return null; // Return null if not found
            }
            return enrolment; // Return the enrolment object
        }

        public async Task<IEnumerable<Enrolment>> GetAllEnrolments()
        {
            // Implementation for retrieving all enrolments
           // var enrolments = await _context.Enrolments.ToListAsync();
           var enrolments = await _context.Enrolments
                .Include(e => e.Student) // Include related Student entity
                .Include(e => e.Course)  // Include related Course entity
                .ToListAsync();
            return enrolments; // Return the list of enrolments
        }

        public async Task UpdateEnrolment(int enrolmentId, Enrolment enrolment)
        {
            // Implementation for updating an enrolment
            var existingEnrolment = await _context.Enrolments.FindAsync(enrolmentId);
            if (existingEnrolment == null)
            {
                return; 
            }
            // Update properties as needed
            existingEnrolment.StudentId = enrolment.StudentId;
            existingEnrolment.CourseId = enrolment.CourseId;
            existingEnrolment.EnrolmentDate = enrolment.EnrolmentDate;
            _context.Enrolments.Update(existingEnrolment);
            await _context.SaveChangesAsync();  
           
        }

        // delete enrolment
        public async Task DeleteEnrolment(int enrolmentid)
        {
            // Implementation for deleting an enrolment
            var enrolment = await _context.Enrolments.FindAsync(enrolmentid);
            if(enrolment == null)
            {
                return; // Return if not found
            }
            _context.Enrolments.Remove(enrolment);  
            await _context.SaveChangesAsync();
        }
        
        }
}
