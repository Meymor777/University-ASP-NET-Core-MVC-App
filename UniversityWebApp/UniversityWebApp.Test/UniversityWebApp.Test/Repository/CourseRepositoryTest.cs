using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UniversityWebApp.Data;
using UniversityWebApp.Models;
using UniversityWebApp.Repository;

namespace UniversityWebApp.Test.Repository
{
    public class CourseRepositoryTests
    {
        private async Task<UniversityDbContext> GetDbContext()
        {
            DbContextOptions<UniversityDbContext> options = new DbContextOptionsBuilder<UniversityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            UniversityDbContext databaseContext = new UniversityDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Courses.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Courses.Add(
                      new Course()
                      {
                          Name = "Test Name",
                          Description = "Test Description"
                      });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Test]
        public async Task CourseRepository_Add_ExpectBool()
        {
            //Arrange
            Course course = new Course()
            {
                Name = "Test Name",
                Description = "Test Description"
            };
            UniversityDbContext dbContext = await GetDbContext();
            CourseRepository courseRepository = new CourseRepository(dbContext);

            //Act
            bool result = courseRepository.Add(course);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task CourseRepository_GetByIdAsync_ExpectCourse()
        {
            //Arrange
            Guid id = new Guid();
            UniversityDbContext dbContext = await GetDbContext();
            CourseRepository courseRepository = new CourseRepository(dbContext);

            //Act
            Task<Course> result = courseRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Course>>();
        }

        [Test]
        public async Task CourseRepository_GetAll_ExpectList()
        {
            //Arrange
            UniversityDbContext dbContext = await GetDbContext();
            CourseRepository courseRepository = new CourseRepository(dbContext);

            //Act
            IEnumerable<Course> result = await courseRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Course>>();
        }

        [Test]
        public async Task CourseRepository_SuccessfulDelete_ExpectTrue()
        {
            //Arrange
            Course course = new Course()
            {
                Name = "Test Name",
                Description = "Test Description"
            };
            UniversityDbContext dbContext = await GetDbContext();
            CourseRepository courseRepository = new CourseRepository(dbContext);

            //Act
            courseRepository.Add(course);
            bool result = courseRepository.Delete(course);

            //Assert
            result.Should().BeTrue();
        }
    }
}