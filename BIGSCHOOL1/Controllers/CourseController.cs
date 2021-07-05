using BIGSCHOOL1.Models;
using BIGSCHOOL1.viewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
                categories = dbContext.categories.ToList()
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
            var course = new course(){
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModels.GetDateTime(),
                CategoryId = viewModels.category,
                Place = viewModels.Place,
            };
            dbContext.coursses.Add(course);
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}