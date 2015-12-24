using System.Web.Mvc;
using ZC_IT_TimeTracking.BusinessEntities.Model;
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
            if (!User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Index", "Home");
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
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = accountService.ValidationErrors.Errors[0].ErrorDescription;
                }
            }
            return View();
        }

        public ActionResult Registration()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterUserViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                AccountService accountService = new AccountService();
                var user = AutoMapper.Mapper.DynamicMap<RegisterUserViewModel, RegisterUser>(registerModel);
                bool isSuccess = accountService.CreateUser(user);
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
            if (ModelState.IsValid)
            {
                AccountService accountService = new AccountService();
                bool isSuccess = accountService.CreateRole(RoleName);
                if (isSuccess)
                {
                    ViewBag.Message = "Role Created Successfully!";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.Message = accountService.ValidationErrors.Errors[0].ErrorDescription;
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            AccountService accountService = new AccountService();
            bool isSuccess = accountService.LogoutUser();
            if (isSuccess)
                return RedirectToAction("Login", "Account");
            return Content(accountService.ValidationErrors.Errors[0].ErrorDescription);
        }
        
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);                
            }
            else
            {
                AccountService accountService = new AccountService();
                accountService.ChangePassword(model.NewPassword);
                
                return View("ChangePassword");
            }
        }
    }
}