using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UniversityWebApp.Data;
using UniversityWebApp.Models;
using UniversityWebApp.Repository;

namespace UniversityWebApp.Test.Repository
{
    public class GroupRepositoryTest
    {
        private async Task<UniversityDbContext> GetDbContext()
        {
            DbContextOptions<UniversityDbContext> options = new DbContextOptionsBuilder<UniversityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            UniversityDbContext databaseContext = new UniversityDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Groups.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Course course = new Course()
                    {
                        Name = "Test Name",
                        Description = "Test Description"
                    };
                    databaseContext.Courses.Add(course);
                    databaseContext.Groups.Add(
                      new Group()
                      {
                          Name = "Test Name",
                          CourseId = course.CourseId
                      });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Test]
        public async Task GroupRepository_Add_ExpectBool()
        {
            //Arrange
            Course course = new Course()
            {
                Name = "Test Name",
                Description = "Test Description"
            };
            Group group = new Group()
            {
                Name = "Test Name",
                CourseId = course.CourseId
            };
            UniversityDbContext dbContext = await GetDbContext();
            CourseRepository courseRepository = new CourseRepository(dbContext);
            GroupRepository groupRepository = new GroupRepository(dbContext);

            //Act
            bool result1 = courseRepository.Add(course);
            bool result2 = groupRepository.Add(group);

            //Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Test]
        public async Task GroupRepository_GetByIdAsync_ExpectGroup()
        {
            //Arrange
            Guid id = new Guid();
            UniversityDbContext dbContext = await GetDbContext();
            GroupRepository groupRepository = new GroupRepository(dbContext);

            //Act
            Task<Group> result = groupRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Group>>();
        }

        [Test]
        public async Task GroupRepository_GetAll_ExpectList()
        {
            //Arrange
            UniversityDbContext dbContext = await GetDbContext();
            GroupRepository groupRepository = new GroupRepository(dbContext);

            //Act
            IEnumerable<Group> result = await groupRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Group>>();
        }

        [Test]
        public async Task GroupRepository_SuccessfulDelete_ExpectTrue()
        {
            //Arrange
            Course course = new Course()
            {
                Name = "Test Name",
                Description = "Test Description"
            };
            Group group = new Group()
            {
                Name = "Test Name",
                CourseId = course.CourseId
            };
            UniversityDbContext dbContext = await GetDbContext();
            CourseRepository courseRepository = new CourseRepository(dbContext);
            GroupRepository groupRepository = new GroupRepository(dbContext);

            //Act
            courseRepository.Add(course);
            groupRepository.Add(group);
            bool result = groupRepository.Delete(group);

            //Assert
            result.Should().BeTrue();
        }
    }
}