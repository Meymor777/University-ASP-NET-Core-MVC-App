using Microsoft.EntityFrameworkCore;
using UniversityWebApp.Data;
using UniversityWebApp.Interfaces;
using UniversityWebApp.Models;

namespace UniversityWebApp.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly UniversityDbContext _context;

        public CourseRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _context
                .Courses
                .Include(c => c.Groups)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
        public async Task<Course> GetByIdAsync(Guid id)
        {
            return await _context
                .Courses
                .Include(c => c.Groups)
                .FirstOrDefaultAsync(c => c.CourseId == id);
        }

        public bool Add(Course course)
        {
            _context.Add(course);
            return Save();
        }
        public bool Update(Course course)
        {
            _context.Update(course);
            return Save();
        }
        public bool Delete(Course course)
        {
            _context.Remove(course);
            return Save();
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
