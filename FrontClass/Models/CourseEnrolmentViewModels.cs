using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontClass.Models
{
    public class CourseEnrolmentViewModel
    {
        public CourseEnrolmentViewModel()
        {
            this.Courses = new Dictionary<Course, bool>();
        }

        public Dictionary<Course, bool> Courses { get; set; }
    }

    public class EnrolInCourseViewModel
    {
        public int CourseId { get; set; }
        public string EntryCode { get; set; }
    }
}