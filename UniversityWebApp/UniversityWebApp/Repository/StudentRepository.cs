using Microsoft.EntityFrameworkCore;
using UniversityWebApp.Data;
using UniversityWebApp.Interfaces;
using UniversityWebApp.Models;

namespace UniversityWebApp.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UniversityDbContext _context;

        public StudentRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _context
                .Students
                .Include(s => s.Group.Course)
                .Include(s => s.Group)
                .OrderBy(c => c.Group.Course.Name)
                .ThenBy(g => g.Group.Name)
                .ThenBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToListAsync();
        }
        public async Task<Student> GetByIdAsync(Guid id)
        {
            return await _context
                .Students
                .Include(s => s.Group.Course)
                .Include(s => s.Group)
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

        public bool Add(Student student)
        {
            _context.Add(student);
            return Save();
        }
        public bool Update(Student student)
        {
            _context.Update(student);
            return Save();
        }
        public bool Delete(Student student)
        {
            _context.Remove(student);
            return Save();
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
