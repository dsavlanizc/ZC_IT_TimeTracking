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
                bool isSuccess = accountService.LoginUser(loginModel.UserName, loginModel.Password, loginModel.RememberMe);
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
                bool isSuccess = accountService.CreateUser(registerModel.UserName, registerModel.Password, registerModel.EmailID, registerModel.RoleName);
                if (isSuccess)
                {
                    ModelState.Clear();
                    ViewBag.Message = "User Created Successfully!";
                    return RedirectToAction("Login", "Account");
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
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(string RoleName)
        {
            if(ModelState.IsValid)
            {
                AccountService accountService = new AccountService();
                bool isSuccess = accountService.CreateRole(RoleName);
                if (isSuccess)
                {
                    ViewBag.Message = "Role Created Successfully!";
                }
                else
                {
                    ViewBag.Message = accountService.ValidationErrors.Errors[0].ErrorDescription;
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            AccountService accountService = new AccountService();
            bool isSuccess = accountService.LogoutUser();
            if (isSuccess)
            {
                return RedirectToAction("Login", "Account");
            }
            return Content(accountService.ValidationErrors.Errors[0].ErrorDescription);
        }
    }
}