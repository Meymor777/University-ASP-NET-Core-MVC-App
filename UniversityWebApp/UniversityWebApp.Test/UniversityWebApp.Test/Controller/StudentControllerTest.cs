using UniversityWebApp.Controllers;
using UniversityWebApp.Interfaces;
using FakeItEasy;
using UniversityWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace UniversityWebApp.Test.Controller
{
    public class StudentControllerTest
    {
        private StudentController _studentController;
        private IStudentRepository _studentRepository;
        private IGroupRepository _groupRepository;

        public StudentControllerTest()
        {
            //Dependencies
            _studentRepository = A.Fake<IStudentRepository>();
            _groupRepository = A.Fake<IGroupRepository>();

            //SUT
            _studentController = new StudentController(_studentRepository, _groupRepository);
        }

        [Test]
        public void StudentController_Index_ExpectSuccess()
        {
            //Arrange - What do i need to bring in?
            IEnumerable<Student> student = A.Fake<IEnumerable<Student>>();
            A.CallTo(() => _studentRepository.GetAll()).Returns(student);
            //Act
            Task<IActionResult> result = _studentController.IndexAsync();
            //Assert - Object check actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Test]
        public void StudentController_Detail_ExpectSuccessAsync()
        {
            //Arrange
            Guid id = new Guid();
            Student student = A.Fake<Student>();
            A.CallTo(() => _studentRepository.GetByIdAsync(id)).Returns(student);
            //Act
            Task<IActionResult> result = _studentController.DetailAsync(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}