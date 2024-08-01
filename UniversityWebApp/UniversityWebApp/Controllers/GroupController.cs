using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityWebApp.Interfaces;
using UniversityWebApp.Models;
using UniversityWebApp.ViewModels;

namespace UniversityWebApp.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;

        private readonly ICourseRepository _courseRepository;

        public GroupController(IGroupRepository groupRepository, ICourseRepository courseRepository)
        {
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;

        }

        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Group> groups = await _groupRepository.GetAll();
            return View(groups);
        }

        public async Task<IActionResult> DetailAsync(Guid id)
        {
            Group? group = await _groupRepository.GetByIdAsync(id);
            return View(group);
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            Group? group = await _groupRepository.GetByIdAsync(id);
            if (group == null)
            {
                return View("Error");
            }
            IEnumerable<Course> allCourse = await _courseRepository.GetAll();
            SelectList courseSelectList = new SelectList(allCourse, "CourseId", "Name");
            ViewBag.CourseSelectList = courseSelectList;
            EditGroupViewModel groupViewModel = new EditGroupViewModel
            {
                Name = group.Name,
                CourseId = group.CourseId
            };
            return View(groupViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(Guid id, EditGroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit group");
                IEnumerable<Course> allCourse = await _courseRepository.GetAll();
                SelectList courseSelectList = new SelectList(allCourse, "CourseId", "Name");
                ViewBag.CourseSelectList = courseSelectList;
                return View("Edit", groupViewModel);
            }
            Group group = new Group
            {
                GroupId = id,
                Name = groupViewModel.Name,
                CourseId = groupViewModel.CourseId
            };
            _groupRepository.Update(group);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Group groupDetails = await _groupRepository.GetByIdAsync(id);
            if (groupDetails == null)
            {
                return View("Error");
            }
            return View(groupDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteGroup(Guid GroupId)
        {
            Group groupDetails = await _groupRepository.GetByIdAsync(GroupId);
            if (groupDetails == null)
            {
                return View("Error");
            }
            if (groupDetails.Students.Count != 0)
            {
                ModelState.AddModelError("Course", "Group can not be deleted if there is at least one student in this group");
                return View("Delete", groupDetails);
            }
            _groupRepository.Delete(groupDetails);
            return RedirectToAction("Index");
        }
    }
}
