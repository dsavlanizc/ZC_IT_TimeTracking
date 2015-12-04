using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            this.RoleList = new List<string>();
        }
        [Required]
        [Display(Name="Role Name")]
        public string RoleName { get; set; }

        [Required]
        [Display(Name = "Role List")]
        public List<string> RoleList { get; set; }
    }
}