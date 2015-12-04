using System.Web.Mvc;
using ZC_IT_TimeTracking.ViewModels;

namespace ZC_IT_TimeTracking.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterUserViewModel registerModel)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}