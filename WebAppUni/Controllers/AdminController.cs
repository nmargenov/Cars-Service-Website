using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppUni.ViewModels.Admin;
using WebAppUni.Repositories;
using WebAppUni.AuthCheck;
using WebAppUni.Entities;

namespace WebAppUni.Controllers
{
    [AuthNotLogged]
    [AdminCheck]
    public class AdminController : Controller
    {

        public ActionResult Index()
        {

            DataBase context = new DataBase();
            IndexVM model = new IndexVM();
            model.Users = context.Users.ToList();
            return View(model);
        }

        public ActionResult MakeAdmin(int id)
        {

            DataBase context = new DataBase();
            User user = new User();
            user = context.Users.Where(u => u.Id == id).FirstOrDefault();

            user.Role = Entities.User.PermissionLevel.Admin;
            context.Users.Update(user);
            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult MakeUser(int id)
        {
            User loggedUser = (User)Session["loggedUser"];

            DataBase context = new DataBase();
            User user = new User();
            user = context.Users.Where(u => u.Id == id).FirstOrDefault();

            user.Role = Entities.User.PermissionLevel.User;
            context.Users.Update(user);
            context.SaveChanges();

            return RedirectToAction("Index", "Admin");

        }

        public ActionResult MakeEmployee(int id)
        {
            User loggedUser = (User)Session["loggedUser"];

            DataBase context = new DataBase();
            User user = new User();
            user = context.Users.Where(u => u.Id == id).FirstOrDefault();

            user.Role = Entities.User.PermissionLevel.Employee;

            context.Users.Update(user);
            context.SaveChanges();

            return RedirectToAction("Index", "Admin");

        }

        [HttpGet]
        public ActionResult CreateUser()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(CreateUserVM model)
        {
            User loggedUser = (User)Session["loggedUser"];


            if (!ModelState.IsValid)
            {
                return View(model);
            }
            DataBase context = new DataBase();

            User user = context.Users.Where(u => u.Username == model.Username).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("usernameFound", "This username is already in use!");
                return View(model);
            }
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("differentPasswords", "Passwords do not match!");
                return View(model);
            }
            if (model.Role != CreateUserVM.PermissionLevel.Admin &&
                model.Role != CreateUserVM.PermissionLevel.User &&
                model.Role != CreateUserVM.PermissionLevel.Employee)
            {
                ModelState.AddModelError("invalidRole", "Invalid Role!");
                return View(model);
            }

            user = new User();
            user.Username = model.Username;
            user.Password = model.Password;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Role = (User.PermissionLevel)model.Role;

            context.Users.Add(user);
            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            User loggedUser = (User)Session["loggedUser"];


            DataBase context = new DataBase();

            User userToEdit = context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (userToEdit == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            EditVM model = new EditVM();
            model.Id = userToEdit.Id;
            model.Username = userToEdit.Username;
            model.Password = userToEdit.Password;
            model.FirstName = userToEdit.FirstName;
            model.LastName = userToEdit.LastName;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditVM model)
        {
            User loggedUser = (User)Session["loggedUser"];


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DataBase context = new DataBase();
            User user = context.Users.Where(u => u.Username == model.Username).FirstOrDefault();


            if (user != null && user.Id != model.Id)
            {
                ModelState.AddModelError("usernameFound", "This username is already in use!");
                return View(model);
            }
            if (model.Role != EditVM.PermissionLevel.Admin &&
                model.Role != EditVM.PermissionLevel.User &&
                model.Role != EditVM.PermissionLevel.Employee)
            {
                ModelState.AddModelError("invalidRole", "Invalid Role!");
                return View(model);
            }
            user.Username = model.Username;
            user.Password = model.Password;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Role = (User.PermissionLevel)model.Role;

            context.Users.Update(user);
            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }


        public ActionResult Delete(int id)
        {
            User loggedUser = (User)Session["loggedUser"];
            


            DataBase context = new DataBase();
            User item = new User();
            item.Id = id;

            if (context.Users.Contains(item))
            {
                foreach(Task task in context.Tasks)
                {
                    if(task.AssigneeId == item.Id)
                    {
                        context.Tasks.Remove(task);
                    }
                }
                context.Users.Remove(item);
                context.SaveChanges();
            }

            return RedirectToAction("Index","Admin");
        }
    }
}