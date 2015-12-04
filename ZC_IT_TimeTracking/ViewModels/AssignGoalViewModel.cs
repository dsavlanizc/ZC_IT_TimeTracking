using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.ViewModels
{
    public class TeamMembersViewModel
    {
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
    }
   
    public class TeamViewModel
    {
        [Required]
        [Display(Name = "Team")]
        public string TeamName { get; set; }

        public List<TeamMembersViewModel> Teammember { get; set; }
    }
    public class AssignGoalViewModel
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

        public TeamViewModel TeamDetail { get; set; }
    }
}