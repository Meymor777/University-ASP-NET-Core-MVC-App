using UniversityWebApp.Models;

namespace UniversityWebApp.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetByIdAsync(Guid id);
        bool Add(Course course);
        bool Update(Course course);
        bool Delete(Course course);
        bool Save();
    }
}
