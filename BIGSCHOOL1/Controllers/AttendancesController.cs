using BIGSCHOOL1.DTOs;
using BIGSCHOOL1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BIGSCHOOL1.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext dbContext;

        public AttendancesController()
        {
            dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDTOs attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (dbContext.attendances.Any(a => a.AttendeeID == userId && a.CourseID == attendanceDto.CourseId))
                return BadRequest("the Attendance already exitss!");
            var attendance = new Attendance
            {
                CourseID = attendanceDto.CourseId,
                AttendeeID = userId
            };
            dbContext.attendances.Add(attendance);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}
