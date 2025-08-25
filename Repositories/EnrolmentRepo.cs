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

        public Course? Course { get; set; } 
        public Student? Student { get; set; }

       
        public async Task AddEnrolment(Enrolment enrolment)
        {
            // Implementation for adding an enrolment
            try
            {
                await _context.Enrolments.AddAsync(enrolment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            
        }

        public async Task<Enrolment> GetEnrolmentById(int enrolmentId)
        {
            // Implementation for retrieving an enrolment by ID
            try
            {
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Enrolment>> GetAllEnrolments()
        {
            // Implementation for retrieving all enrolments
            try
            {
                // var enrolments = await _context.Enrolments.ToListAsync();
                var enrolments = await _context.Enrolments
                     .Include(e => e.Student) // Include related Student entity
                     .Include(e => e.Course)  // Include related Course entity
                     .ToListAsync();
                return enrolments; // Return the list of enrolments 
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateEnrolment(int enrolmentId, Enrolment enrolment)
        {
            // Implementation for updating an enrolment
            try
            {
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
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
           
        }

        // delete enrolment
        public async Task DeleteEnrolment(int enrolmentid)
        {
            // Implementation for deleting an enrolment
            try
            {
                var enrolment = await _context.Enrolments.FindAsync(enrolmentid);
                if (enrolment == null)
                {
                    return; // Return if not found
                }
                _context.Enrolments.Remove(enrolment);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        }
}
