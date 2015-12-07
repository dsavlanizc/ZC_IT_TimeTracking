using System.Web.Mvc;
using ZC_IT_TimeTracking.Services.Account;
using ZC_IT_TimeTracking.Services.Role;
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
                AccountService accountService = new AccountService();
                bool isSuccess = accountService.LoginUser(loginModel.UserName, loginModel.Password);
                if (isSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Invalid username or password";
                }
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
                AccountService accountService = new AccountService();
                bool isSuccess = accountService.CreateUser(registerModel.UserName, registerModel.Password);
                if (isSuccess)
                {
                    ModelState.Clear();
                    ViewBag.Message = "User Created Successfully!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = accountService.ValidationErrors.Errors[0].ErrorDescription;
                }
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