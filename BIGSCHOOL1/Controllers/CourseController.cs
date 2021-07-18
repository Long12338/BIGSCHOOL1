using BIGSCHOOL1.Models;
using BIGSCHOOL1.viewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIGSCHOOL1.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public CourseController()
        {
            dbContext = new ApplicationDbContext();
        }



        // GET: Course
        [Authorize]
        public ActionResult Create()
        {
            var viewModels = new CourseViewModels
            {
                categories = dbContext.categories.ToList(),
                Heading = "Add Course"
            };
            return View(viewModels);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModels viewModels)
        {
            if (!ModelState.IsValid)
            {
                viewModels.categories = dbContext.categories.ToList();
                return View("create", viewModels);
            }
            var course = new course()
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModels.GetDateTime(),
                CategoryId = viewModels.category,
                Place = viewModels.Place,
               
            };
            dbContext.courses.Add(course);
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var courses = dbContext.attendances
                .Where(a => a.AttendeeID == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();
            var viewModel = new CoursesViewModels
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }
        [Authorize]
        public ActionResult mine()
        {
            var userId = User.Identity.GetUserId();
            var courses = dbContext.courses
               .Where(c => c.LecturerId == userId && c.DateTime > DateTime.Now)
               .Include(l => l.Lecturer)
               .Include(l => l.Category)
               .ToList();
            return View(courses);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = dbContext.courses.Single(c => c.Id == id && c.LecturerId == userId);
            var viewModel = new CourseViewModels
            {
                categories = dbContext.categories.ToList(),
                Date = course.DateTime.ToString("dd/M/yyyy"),
                Time = course.DateTime.ToString("H:mm"),
                category = course.CategoryId,
                Place = course.Place,
                Heading = "Edit Course",
                Id=course.Id
            };
            return View("Create", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModels viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.categories = dbContext.categories.ToList();
                return View("Create", viewModel);
            }
            var useId = User.Identity.GetUserId();
            var course = dbContext.courses.Single(c => c.Id ==viewModel.Id && c.LecturerId == useId);
            course.Place = viewModel.Place;
            course.DateTime = viewModel.GetDateTime();
            course.CategoryId = viewModel.category;
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}