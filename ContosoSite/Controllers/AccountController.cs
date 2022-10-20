using ContosoSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace ContosoSite.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginViewModel loggedInUser)
        {
            if (ModelState.IsValid)
            {
                var ValidUser = IsValidUser(loggedInUser);
                if (ValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(loggedInUser.Email, false);
                    Session["UserName"] = ValidUser.Email;
                    Session["UserRole"] = ValidUser.Role;
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("Faliure", "Wrong Username And password Combination");
                    return View();
                }
            }
            else
            {
                return View(loggedInUser);
            }

        }

        public ContosoUser IsValidUser(LoginViewModel model)
        {
            using (var dbContext = new ContosoUniversityDataEntities())
            {
                ContosoUser user = dbContext.ContosoUsers.Where(query => query.Email.Equals(model.Email) &&
                query.Password.Equals(model.Password)).SingleOrDefault();

                if (user == null)
                    return null;
                else
                    return user;
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");

        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public ActionResult SaveContosoUser(ContosoUser regUser)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new ContosoUniversityDataEntities())
                {

                    ContosoUser user = dbContext.ContosoUsers.Where(query => query.Email.Equals(regUser.Email)).FirstOrDefault(); 
                    if (user == null)
                    {
                        ContosoUser contosoUser = new ContosoUser();
                        contosoUser.FirstName = regUser.FirstName;
                        contosoUser.LastName = regUser.LastName;
                        contosoUser.Email = regUser.Email;
                        contosoUser.Password = regUser.Password;
                        contosoUser.Role = regUser.Role;
                        UpdateModel(contosoUser);
                        dbContext.ContosoUsers.Add(contosoUser);

                        dbContext.SaveChanges();

                        ViewBag.Message = "User Saved";

                    }
                    else
                    {
                        ViewBag.Message = "Invalid User";
                    }
                    return View("Login");
                }
            }
            else
            {
                return View("Register", regUser);
            }

        }
    }
}