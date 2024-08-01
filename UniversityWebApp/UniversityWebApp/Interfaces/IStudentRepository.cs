using UniversityWebApp.Models;

namespace UniversityWebApp.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetByIdAsync(Guid id);
        bool Add(Student student);
        bool Update(Student student);
        bool Delete(Student student);
        bool Save();
    }
}
