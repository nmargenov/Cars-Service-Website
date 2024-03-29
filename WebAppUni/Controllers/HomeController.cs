using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppUni.ViewModels.Home;
using System.Web.Mvc;
using WebAppUni.Repositories;
using WebAppUni.Entities;
using WebAppUni.AuthCheck;

namespace WebAppUni.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataBase context = new DataBase();
            IndexVM model = new IndexVM();
            model.Users = context.Users.Where(u => u.Role == Entities.User.PermissionLevel.Employee || u.Role == Entities.User.PermissionLevel.Admin).ToList();
            return View(model);
        }

        [HttpGet]
        [AuthLogged]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM model)
        { 
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            DataBase context = new DataBase();
            User loggedUser = context.Users.Where(u => u.Username == model.Username &&
                                                           u.Password == model.Password).FirstOrDefault();

            if(loggedUser == null)
            {
                this.ModelState.AddModelError("authError", "Invalid username or password!");
                return View(model);
            }
            Session["loggedUser"] = loggedUser;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            if (Session["loggedUser"] != null)
            {
                Session["loggedUser"] = null;
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AuthLogged]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DataBase context = new DataBase();

            User existingUsername = context.Users.Where(u => u.Username == model.Username).FirstOrDefault();

            if(existingUsername != null)
            {
                ModelState.AddModelError("usernameFound", "This username is already in use!");
                return View(model);
            }
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("differentPasswords", "Passwords do not match!");
                return View(model);
            }

            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Role = Entities.User.PermissionLevel.User;

            context.Users.Add(item);
            context.SaveChanges();

            Session["loggedUser"] = item;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult Policy()
        {
            return View();
        }
        
        public ActionResult About()
        {
            return View();
        }

    }
}