using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZC_IT_TimeTracking.ViewModels;

namespace ZC_IT_TimeTracking.ViewModels
{
   
    public class GoalViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string GoalTitle { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string GoalDescription { get; set; }

        public QuarterViewModel GoalQuarter { get; set; }

        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }

        [Required]
        [Display(Name = "Measurement Value")]
        public int MeasurementValue { get; set; }
    }

    public class GoalListViewModel
    {
        public GoalViewModel ViewGoal { get; set; }

        [Required]
        [Display(Name = "Create Date")]
        public DateTime Creation_Date { get; set; }
    }

    public class ViewGoalViewModel
    {
        public GoalViewModel ViewGoal { get; set; }
        
        [Required]
        [Display(Name = "Is Higher Value Appreciable?")]
        public bool IsHigherValueGood { get; set; }
    }
    
    public class CreateGoalViewModel
    {
        public ViewGoalViewModel CreateGoal { get; set; }
    }

    public class EditGoalViewModel
    {
        public ViewGoalViewModel EditGoal { get; set; }
    }

    public class ViewGoalRulesViewModel
    {
        [Required]
        [Display(Name = "Range From")]
        public int RangeFrom { get; set; }

        [Required]
        [Display(Name = "Range To")]
        public int RangeTo { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int Rating { get; set; }
    }

    public class CreateGoalRuleViewModel
    {
        public ViewGoalRulesViewModel CreateGoalRule { get; set; }
    }
    public class EditeGoalRuleViewModel
    {
        public ViewGoalRulesViewModel GoalRules { get; set; }
    }
}