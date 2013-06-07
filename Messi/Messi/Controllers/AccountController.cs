using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Messi.ViewModels;

namespace Messi.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (var messi = new Models.Messi())
                {
                    var user = (from u in messi.Users
                               where u.UserName == model.UserName && u.Password==model.Password
                               select u).FirstOrDefault();
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName+"|"+user.UserId,false);
                        return RedirectToLocal(returnUrl);
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                using (var messi = new Models.Messi())
                {
                    var user = (from u in messi.Users
                                where u.UserName == model.UserName
                                select u).FirstOrDefault();
                    if (user == null)
                    {
                        user = new Models.User()
                            {
                                CountryName = model.CountryName,
                                UserName = model.UserName,
                                Password = model.Password,
                                DisplayName = model.DisplayName
                            };
                        messi.Entry(user).State=EntityState.Added;
                        messi.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Name already taken!!!");
                        return View(model);
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}
