using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppUni.AuthCheck;
using WebAppUni.Entities;
using WebAppUni.Repositories;
using WebAppUni.ViewModels.Tasks;

namespace WebAppUni.Controllers
{
    [AuthNotLogged]
    [AdminCheck]
    public class TasksController : Controller
    {
        public ActionResult Index(int id)
        {
            User loggedUser = (User)Session["loggedUser"];
          

            DataBase context = new DataBase();


            IndexVM model = new IndexVM();
            model.Car = context.Cars
                                        .Where(c => c.Id == id)
                                        .FirstOrDefault();

            if (model.Car == null)
                return RedirectToAction("Index", "ManageCars");

            model.Items = context.Tasks
                                    .Where(t => t.CarId == model.Car.Id)
                                    .ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create(int carId)
        {
            User loggedUser = (User)Session["loggedUser"];
          

            DataBase context = new DataBase();

            CreateVM model = new CreateVM();
            model.CarId = carId;
            model.OwnerId = loggedUser.Id;

            model.Users = context.Users.Where(u=>u.Role != Entities.User.PermissionLevel.User).ToList();

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(CreateVM model)
        {
            DataBase context = new DataBase();
            model.Users = context.Users.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Car car = new Car();
            car.Id = model.CarId;
            if (!context.Cars.Contains(car))
            {
                ModelState.AddModelError("invalidCar", "The car you tried to assign the task is not valid!");
                return View(model);
            }

            User user = new User();
            user.Id = model.AssigneeId;
            if (!context.Users.Contains(user))
            {
                ModelState.AddModelError("invalidUser", "The user you tried to assign the task is not valid!");
                return View(model);
            }

            Task task = new Task();
            task.CarId = model.CarId;
            task.OwnerId = model.OwnerId;
            task.AssigneeId = model.AssigneeId;
            task.Title = model.Title;
            task.Description = model.Description;

            context.Tasks.Add(task);
            context.SaveChanges();

            return RedirectToAction("Index", "ManageCars");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            User loggedUser = (User)Session["loggedUser"];
           
            DataBase context = new DataBase();
            Task task = context.Tasks.Where(t => t.Id == id).FirstOrDefault();

            if(task == null)
            {
                return RedirectToAction("Index", "ManageCars");

            }

            EditVM model = new EditVM();
            model.Id = task.Id;
            model.AssigneeId = task.AssigneeId;
            model.CarId = task.CarId;
            model.OwnerId = task.OwnerId;
            model.Title = task.Title;
            model.Description = task.Description;
            model.Users = context.Users.Where(u => u.Role != Entities.User.PermissionLevel.User).ToList();


            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditVM model)
        {
            DataBase context = new DataBase();

            model.Users = context.Users.ToList();
            User loggedUser = (User)Session["loggedUser"];
           

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Task task = context.Tasks.Where(t => t.Id == model.Id).FirstOrDefault();
            User user = new User();
            user.Id = model.AssigneeId;
            if (!context.Users.Contains(user))
            {
                ModelState.AddModelError("invalidUser", "The user you tried to assign the task is not valid!");
                return View(model);
            }

            task.Title = model.Title;
            task.Description = model.Description;
            task.AssigneeId = model.AssigneeId;

            context.Tasks.Update(task);
            context.SaveChanges();

            return RedirectToAction("Index", "ManageCars");
        }
        


        public ActionResult Delete(int id)
        {
           

            DataBase context = new DataBase();
            Task task = new Task();
            task.Id = id;
            if (context.Tasks.Contains(task))
            {
                context.Tasks.Remove(task);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "ManageCars");
        }
    }
}