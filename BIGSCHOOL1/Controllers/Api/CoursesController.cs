using BIGSCHOOL1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BIGSCHOOL1.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext dbContext { get; set; }
        public CoursesController()
        {
            dbContext = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = dbContext.courses.Single(c => c.Id == id && c.LecturerId == userId);
            if (course.IsCancel)
                return NotFound();
            course.IsCancel = true;
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
