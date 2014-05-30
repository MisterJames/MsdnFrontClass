using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontClass.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [Display(Name="Course Title")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name="Entry Code")]
        public string EntryCode { get; set; }
        
        public virtual HashSet<Module> Modules { get; set; }

    }
}