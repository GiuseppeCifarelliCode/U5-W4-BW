using CuraVet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CuraVet.Controllers
{
    public class AuthController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            User u = db.User.SingleOrDefault(x => x.Username == user.Username);
            if (u.Username != null && u.Password == user.Password)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                if (u.Ruolo == "Vet") return RedirectToAction("Index", "Clinica");
                else return RedirectToAction("Index", "Farmacia");
            }
            else
            {
                ViewBag.Error = "Username e Password non coincidono!";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}