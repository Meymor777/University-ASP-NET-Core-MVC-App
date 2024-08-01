using Microsoft.EntityFrameworkCore;
using UniversityWebApp.Data;
using UniversityWebApp.Interfaces;
using UniversityWebApp.Models;

namespace UniversityWebApp.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly UniversityDbContext _context;

        public GroupRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            return await _context
                .Groups
                .Include(g => g.Course)
                .Include(g => g.Students)
                .OrderBy(c => c.Course.Name)
                .ThenBy(g => g.Name)
                .ToListAsync();
        }
        public async Task<Group> GetByIdAsync(Guid id)
        {
            return await _context
                .Groups
                .Include(g => g.Course)
                .Include(g => g.Students)
                .FirstOrDefaultAsync(g => g.GroupId == id);
        }

        public bool Add(Group group)
        {
            _context.Add(group);
            return Save();
        }
        public bool Update(Group group)
        {
            _context.Update(group);
            return Save();
        }
        public bool Delete(Group group)
        {
            _context.Remove(group);
            return Save();
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
