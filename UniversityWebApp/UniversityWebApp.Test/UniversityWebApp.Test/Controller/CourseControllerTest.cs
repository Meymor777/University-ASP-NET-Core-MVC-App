using UniversityWebApp.Controllers;
using UniversityWebApp.Interfaces;
using FakeItEasy;
using UniversityWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace UniversityWebApp.Test.Controller
{
    public class CourseControllerTest
    {
        private CourseController _courseController;
        private ICourseRepository _courseRepository;

        public CourseControllerTest()
        {
            //Dependencies
            _courseRepository = A.Fake<ICourseRepository>();

            //SUT
            _courseController = new CourseController(_courseRepository);
        }

        [Test]
        public void CourseController_Index_ExpectSuccess()
        {
            //Arrange - What do i need to bring in?
            IEnumerable<Course> course = A.Fake<IEnumerable<Course>>();
            A.CallTo(() => _courseRepository.GetAll()).Returns(course);
            //Act
            Task<IActionResult> result = _courseController.IndexAsync();
            //Assert - Object check actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Test]
        public void CourseController_Detail_ExpectSuccessAsync()
        {
            //Arrange
            Guid id = new Guid();
            Course course = A.Fake<Course>();
            A.CallTo(() => _courseRepository.GetByIdAsync(id)).Returns(course);
            //Act
            Task<IActionResult> result = _courseController.DetailAsync(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}