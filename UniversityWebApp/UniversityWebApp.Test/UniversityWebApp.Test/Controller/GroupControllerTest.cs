using UniversityWebApp.Controllers;
using UniversityWebApp.Interfaces;
using FakeItEasy;
using UniversityWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace UniversityWebApp.Test.Controller
{
    public class GroupControllerTest
    {
        private GroupController _groupController;
        private IGroupRepository _groupRepository;
        private ICourseRepository _courseRepository;

        public GroupControllerTest()
        {
            //Dependencies
            _groupRepository = A.Fake<IGroupRepository>();
            _courseRepository = A.Fake<ICourseRepository>();

            //SUT
            _groupController = new GroupController(_groupRepository, _courseRepository);
        }

        [Test]
        public void GroupController_Index_ExpectSuccess()
        {
            //Arrange - What do i need to bring in?
            IEnumerable<Group> group = A.Fake<IEnumerable<Group>>();
            A.CallTo(() => _groupRepository.GetAll()).Returns(group);
            //Act
            Task<IActionResult> result = _groupController.IndexAsync();
            //Assert - Object check actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Test]
        public void GroupController_Detail_ExpectSuccessAsync()
        {
            //Arrange
            Guid id = new Guid();
            Group group = A.Fake<Group>();
            A.CallTo(() => _groupRepository.GetByIdAsync(id)).Returns(group);
            //Act
            Task<IActionResult> result = _groupController.DetailAsync(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}