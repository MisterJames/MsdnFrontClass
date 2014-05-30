using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontClass.Models
{
    public class Step
    {
        public int StepId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual int ModuleId { get; set; }
        public virtual Module Module { get; set; }
    }
}