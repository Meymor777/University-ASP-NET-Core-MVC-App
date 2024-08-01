using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityWebApp.Interfaces;
using UniversityWebApp.Models;
using UniversityWebApp.ViewModels;

namespace UniversityWebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        private readonly IGroupRepository _groupRepository;

        public StudentController(IStudentRepository studentRepository, IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Student> students = await _studentRepository.GetAll();
            return View(students);
        }

        public async Task<IActionResult> DetailAsync(Guid id)
        {
            Student? student = await _studentRepository.GetByIdAsync(id);
            return View(student);
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            Student? student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return View("Error");
            }
            IEnumerable<Group> allGroup = await _groupRepository.GetAll();
            SelectList groupSelectList = new SelectList(allGroup, "GroupId", "Name");
            ViewBag.GroupSelectList = groupSelectList;
            EditStudentViewModel studentViewModel = new EditStudentViewModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                GroupId = student.GroupId
            };
            return View(studentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(Guid id, EditStudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit student");
                IEnumerable<Group> allGroup = await _groupRepository.GetAll();
                SelectList groupSelectList = new SelectList(allGroup, "GroupId", "Name");
                ViewBag.GroupSelectList = groupSelectList;
                return View("Edit", studentViewModel);
            }
            Student student = new Student
            {
                StudentId = id,
                FirstName = studentViewModel.FirstName,
                LastName = studentViewModel.LastName,
                GroupId = studentViewModel.GroupId
            };
            _studentRepository.Update(student);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Student studentDetails = await _studentRepository.GetByIdAsync(id);
            if (studentDetails == null)
            {
                return View("Error");
            }
            return View(studentDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteGroup(Guid StudentId)
        {
            Student studentDetails = await _studentRepository.GetByIdAsync(StudentId);
            if (studentDetails == null)
            {
                return View("Error");
            }
            _studentRepository.Delete(studentDetails);
            return RedirectToAction("Index");
        }
    }
}
