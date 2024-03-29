using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppUni.AuthCheck;
using WebAppUni.ViewModels.MyProfile;
using WebAppUni.Repositories;
using WebAppUni.Entities;
using System.Data.Entity;

namespace WebAppUni.Controllers
{
    [AuthNotLogged]
    public class MyProfileController : Controller
    {
        public ActionResult Index()
        {

            DataBase context = new DataBase();
            IndexVM model = new IndexVM();
            User loggedUser = (User)Session["loggedUser"];
            model.User = context.Users.Where(u => u.Id == loggedUser.Id).FirstOrDefault();

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            DataBase context = new DataBase();
            User userToEdit = context.Users.Where(u => u.Id == id).FirstOrDefault();
            User loggedUser = (User)Session["loggedUser"];

            if(userToEdit == null)
            {
                return RedirectToAction("Index", "MyProfile");

            }
            if (userToEdit.Id != loggedUser.Id)
            {
                return RedirectToAction("Index", "MyProfile");
            }
            EditVM model = new EditVM();

            model.Id = userToEdit.Id;
            model.passwordCheck = userToEdit.Password;
            model.Username = userToEdit.Username;
            model.FirstName = userToEdit.FirstName;
            model.LastName = userToEdit.LastName;
            model.Role = (EditVM.PermissionLevel)userToEdit.Role;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DataBase context = new DataBase();
            User loggedUser = (User)Session["loggedUser"];

            if(model.Id != loggedUser.Id)
            {
                return View(model);
            }
            User existingUser = context.Users.Where(u => u.Username == model.Username).FirstOrDefault();

            if (existingUser != null && existingUser.Username != loggedUser.Username)
            {
                ModelState.AddModelError("usernameFound", "This username is already in use!");
                return View(model);
            }

            if (model.passwordCheck != model.OldPassword)
            {
                ModelState.AddModelError("oldPasswordNotMatch", "Incorrect password!");
                return View(model);
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("newPasswordNotMatch", "New password does not match!");
                return View(model);
            }
            context.Dispose();

            DataBase contextNew = new DataBase();
;
            User user = new User();
            user.Id = model.Id;
            user.Username = model.Username;
            user.Password = model.NewPassword;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Role = (User.PermissionLevel)model.Role;

            contextNew.Users.Update(user);
            contextNew.SaveChanges();

            Session["loggedUser"] = user;
            return RedirectToAction("Index", "MyProfile");
        }



        public ActionResult Delete(int id)
        {
            DataBase context = new DataBase();

            User loggedUser = (User)Session["loggedUser"];

            if(loggedUser.Id != id)
            {
                return RedirectToAction("Index", "MyProfile");
            }

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

            Session["loggedUser"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}