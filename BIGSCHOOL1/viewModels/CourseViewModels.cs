using BIGSCHOOL1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BIGSCHOOL1.viewModels
{
    public class CourseViewModels
    {
        [Required]
        public string Place { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        [Validtime]
        public byte category { get; set; }
        public IEnumerable<category> categories { get; set; }
        public DateTime GetDateTime()
        {
            return DateTime.Parse(String.Format("{ 0} { 1}", Date, Time));
        }
    }
}