using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name="Role Name")]
        public string RoleName { get; set; }
    }
}