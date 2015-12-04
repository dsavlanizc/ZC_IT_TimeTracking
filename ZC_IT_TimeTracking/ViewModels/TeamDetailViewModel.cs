using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.ViewModels
{
    public class TeamMembersViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class TeamViewModel : TeamMembersViewModel
    {
        [Required]
        [Display(Name = "Team")]
        public string TeamName { get; set; }

       // public List<TeamMembersViewModel> TeamMember { get; set; }
    }
}