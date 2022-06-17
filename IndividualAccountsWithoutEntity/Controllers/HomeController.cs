using AuthDemo1.Models.Auth;
using AuthDemo1.Models.CustomAttributes;
using AuthDemo1.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuthDemo1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private SignInManager _signInManager;
        private UserManager _authManager;

        public SignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public UserManager AuthManager
        {
            get
            {
                return _authManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set
            {
                _authManager = value;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [IsAuthorized]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await AuthManager.FindAsync(model.Email, model.Password);

            if (user != null)
            {
                await SignInManager.SignInAsync(user, true, model.RememberMe);
                return RedirectToAction("Index", "Dashboard");

            }
            else
            {
                ModelState.AddModelError("", "Korisnik ne postoji u bazi.");
                return View(model);
            }

        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

    }
}