using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZC_IT_TimeTracking.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseEntities DbContext = new DatabaseEntities();
        // GET: Home
        public ActionResult Index()
        {
            var GoalList = DbContext.Goal_Master.ToList();
            return View(GoalList);
        }
    }
}