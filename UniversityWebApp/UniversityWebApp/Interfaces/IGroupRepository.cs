using UniversityWebApp.Models;

namespace UniversityWebApp.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAll();
        Task<Group> GetByIdAsync(Guid id);
        bool Add(Group group);
        bool Update(Group group);
        bool Delete(Group group);
        bool Save();
    }
}
