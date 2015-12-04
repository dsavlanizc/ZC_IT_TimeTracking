using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.ViewModels
{

    public class AssignGoalViewModel : TeamViewModel
    {   
        [Required]
        [Display(Name = "Title")]
        public string GoalTitle { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string  GoalDescription { get; set; }

        [Required]
        [Display(Name = "Weight")]
        public int Weight { get; set; }

      //  public TeamViewModel TeamDetail { get; set; }
    }

    public class ViewAssignedGoalViewModel
    {
        [Required]
        [Display(Name = "Resource Goal Id")]
        public int Resource_GoalID { get; set; }

        [Required]
        [Display(Name = "Goal Title")]
        public string GoalTitle { get; set; }

        [Required]
        [Display(Name = "Weight")]
        public int Weight { get; set; }
    }
}