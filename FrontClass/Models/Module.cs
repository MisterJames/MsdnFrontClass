using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontClass.Models
{
    public class Module
    {
        public int ModuleId { get; set; }

        [Required]
        [Display(Name = "Module Title")]
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public virtual HashSet<Step> Steps { get; set; }

    }
}