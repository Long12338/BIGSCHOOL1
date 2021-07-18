using BIGSCHOOL1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BIGSCHOOL1.viewModels;

namespace BIGSCHOOL1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext dbContext;
        public HomeController()
        {
            dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingCourses = dbContext.courses.Include(c =>c.Lecturer).Include(c => c.Category)
            .Where(c => c.DateTime > DateTime.Now);
            var viewModel = new CoursesViewModels
            {
                UpcommingCourses = upcomingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}