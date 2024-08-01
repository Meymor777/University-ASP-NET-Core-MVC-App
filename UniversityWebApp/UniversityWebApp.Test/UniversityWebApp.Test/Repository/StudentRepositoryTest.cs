using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UniversityWebApp.Data;
using UniversityWebApp.Models;
using UniversityWebApp.Repository;

namespace UniversityWebApp.Test.Repository
{
    public class StudentRepositoryTest
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
                    Group group = new Group()
                    {
                        Name = "Test Name",
                        CourseId = course.CourseId
                    };

                    databaseContext.Courses.Add(course);
                    databaseContext.Groups.Add(group);
                    databaseContext.Students.Add(
                    new Student
                    {
                        FirstName = "Test FirstName",
                        LastName = "Test LastName",
                        GroupId = group.GroupId
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Test]
        public async Task StudentRepository_Add_ExpectBool()
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
            Student student = new Student
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName",
                GroupId = group.GroupId
            };
            UniversityDbContext dbContext = await GetDbContext();
            CourseRepository courseRepository = new CourseRepository(dbContext);
            GroupRepository groupRepository = new GroupRepository(dbContext);
            StudentRepository studentRepository = new StudentRepository(dbContext);

            //Act
            bool result1 = courseRepository.Add(course);
            bool result2 = groupRepository.Add(group);
            bool result3 = studentRepository.Add(student);

            //Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        [Test]
        public async Task StudentRepository_GetByIdAsync_ExpectStudent()
        {
            //Arrange
            Guid id = new Guid();
            UniversityDbContext dbContext = await GetDbContext();
            StudentRepository studentRepository = new StudentRepository(dbContext);

            //Act
            Task<Student> result = studentRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Student>>();
        }

        [Test]
        public async Task StudentRepository_GetAll_ExpectList()
        {
            //Arrange
            UniversityDbContext dbContext = await GetDbContext();
            StudentRepository studentRepository = new StudentRepository(dbContext);

            //Act
            IEnumerable<Student> result = await studentRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Student>>();
        }

        [Test]
        public async Task StudentRepository_SuccessfulDelete_ExpectTrue()
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
            Student student = new Student
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName",
                GroupId = group.GroupId
            };
            UniversityDbContext dbContext = await GetDbContext();
            CourseRepository courseRepository = new CourseRepository(dbContext);
            GroupRepository groupRepository = new GroupRepository(dbContext);
            StudentRepository studentRepository = new StudentRepository(dbContext);

            //Act
            courseRepository.Add(course);
            groupRepository.Add(group);
            studentRepository.Add(student);
            bool result = studentRepository.Delete(student);

            //Assert
            result.Should().BeTrue();
        }
    }
}
