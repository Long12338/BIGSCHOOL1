using BIGSCHOOL1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIGSCHOOL1.viewModels
{
    public class CoursesViewModels
    {
        public IEnumerable<course> UpcommingCourses { get; set; }
        public bool ShowAction { get; set; }
    }
}