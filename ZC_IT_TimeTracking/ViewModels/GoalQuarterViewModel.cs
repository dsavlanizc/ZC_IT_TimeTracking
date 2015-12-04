using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.ViewModels
{
     public class QuarterViewModel
    {
        [Required]
        [Display(Name = "Quarter")]
        public int GoalQuarter { get; set; }

        [Required]
        [Display(Name = "Year")]
        public int QuarterYear { get; set; }
    }
     public class GoalQuarterViewModel : GoalQuarterViewModel
    {
       // public GoalQuarterViewModel GoalQuarter { get; set; }

        [Required]
        [Display(Name = "Create from")]
        public DateTime GoalCreateFrom { get; set; }

        [Required]
        [Display(Name = "Create To")]
        public DateTime GoalCreateTo { get; set; }
    }
}