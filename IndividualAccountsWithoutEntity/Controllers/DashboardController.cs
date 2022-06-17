using AuthDemo1.Models;
using AuthDemo1.Models.Auth;
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
    public class DashboardController : Controller
    {
        private UserManager _authManager;
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

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.Name;
            User model = await AuthManager.FindByNameAsync(userId);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Administration()
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult Basic()
        {
            return View();
        }
    }
}