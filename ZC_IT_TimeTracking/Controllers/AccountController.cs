using System.Web.Mvc;
using ZC_IT_TimeTracking.ViewModels;
using ZC_IT_TimeTracking.Services.Role;

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
            RoleService roleService = new RoleService();
            ViewBag.RoleList = roleService.GetAvailableRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterUserViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                RoleService roleService = new RoleService();
                bool b = roleService.CreateUser(registerModel.UserName, registerModel.Password);
            }
            return View();
        }

        public ActionResult CreateRole()
        {
            RoleService roleService = new RoleService();
            RoleViewModel roleView = new RoleViewModel();
            roleView.RoleList = roleService.GetAvailableRoles();
            return View(roleView);
        }

        [HttpPost]
        public ActionResult CreateRole(RoleViewModel roleView)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }
    }
}