using Microsoft.AspNetCore.Mvc;
using UniversityWebApp.Interfaces;
using UniversityWebApp.Models;
using UniversityWebApp.ViewModels;

namespace UniversityWebApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Course> courses = await _courseRepository.GetAll();
            return View(courses);
        }

        public async Task<IActionResult> DetailAsync(Guid id)
        {
            Course? course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return View("Error");
            }
            return View(course);

        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            Course? course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return View("Error");
            }
            EditCourseViewModel courseViewModel = new EditCourseViewModel
            {
                Name = course.Name,
                Description = course.Description
            };
            return View(courseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(Guid id, EditCourseViewModel editCourseViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit course");
                return View("Edit", editCourseViewModel);
            }
            Course course = new Course
            {
                CourseId = id,
                Name = editCourseViewModel.Name,
                Description = editCourseViewModel.Description
            };
            _courseRepository.Update(course);
            await Task.CompletedTask;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Course courseDetails = await _courseRepository.GetByIdAsync(id);
            if (courseDetails == null)
            {
                return View("Error");
            }
            return View(courseDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCourse(Guid CourseId)
        {
            Course courseDetails = await _courseRepository.GetByIdAsync(CourseId);
            if (courseDetails == null)
            {
                return View("Error");
            }
            if (courseDetails.Groups.Count != 0)
            {
                ModelState.AddModelError("Description", "Course can not be deleted if there is at least one group in this course");
                return View("Delete", courseDetails);
            }
            _courseRepository.Delete(courseDetails);
            return RedirectToAction("Index");
        }

    }
}
