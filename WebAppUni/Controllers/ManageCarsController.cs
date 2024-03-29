using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppUni.AuthCheck;
using WebAppUni.Entities;
using WebAppUni.Repositories;
using WebAppUni.ViewModels.ManageCars;

namespace WebAppUni.Controllers
{
    [AuthNotLogged]
    [AdminCheck]
    public class ManageCarsController : Controller
    {
        public ActionResult Index()
        {
           
            DataBase context = new DataBase();

            IndexVM model = new IndexVM();
            model.Cars = context.Cars.ToList();

            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult Create(AddCarVM model)
        {
            User loggedUser = (User)Session["loggedUser"];
           
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DataBase context = new DataBase();

            Car car = new Car();

            car.AddedByUserId = loggedUser.Id;
            car.Brand = model.Brand;
            car.Model = model.CarModel;
            car.HorsePower = model.HorsePower;
            car.YearMade = model.YearMade;
            car.IsRunning = model.IsRunning;

            context.Cars.Add(car);
            context.SaveChanges();

            return RedirectToAction("Index", "ManageCars");
        }

        [HttpGet] 
        public ActionResult Edit(int id)
        {

            User loggedUser = (User)Session["loggedUser"];
           

            DataBase context = new DataBase();

            Car carToEdit = context.Cars.Where(c => c.Id == id).FirstOrDefault();
            if (carToEdit== null)
            {
                return RedirectToAction("Index", "ManageCars");
            }

            EditVM model = new EditVM();
            model.Id = carToEdit.Id;
            model.AddedByUserId = carToEdit.AddedByUserId;
            model.Brand = carToEdit.Brand;
            model.CarModel = carToEdit.Model;
            model.HorsePower = carToEdit.HorsePower;
            model.YearMade = carToEdit.YearMade;
            model.IsRunning = carToEdit.IsRunning;

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

            Car carToEdit = new Car();
            carToEdit.Id = model.Id;
            carToEdit.AddedByUserId = model.AddedByUserId;
            carToEdit.Brand = model.Brand;
            carToEdit.Model = model.CarModel;
            carToEdit.YearMade = model.YearMade;
            carToEdit.HorsePower = model.HorsePower;
            carToEdit.IsRunning = model.IsRunning;

            context.Cars.Update(carToEdit);
            context.SaveChanges();

            return RedirectToAction("Index", "ManageCars");
        }


        public ActionResult Delete(int id)
        {          

            DataBase context = new DataBase();
            Car carToDelete = new Car();
            carToDelete.Id = id;

            if (context.Cars.Contains(carToDelete))
            {
                foreach (Task task in context.Tasks)
                {
                    if (task.CarId == carToDelete.Id)
                    {
                        context.Tasks.Remove(task);
                    }
                }
                context.Cars.Remove(carToDelete);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "ManageCars");
        }
    }
}