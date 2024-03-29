using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppUni.AuthCheck;
using WebAppUni.Entities;
using WebAppUni.Repositories;
using WebAppUni.ViewModels.MyCars;

namespace WebAppUni.Controllers
{
    [AuthNotLogged]
    [EmployeeCheck]
    public class MyCarsController : Controller
    {
        public ActionResult Index()
        {
            DataBase context = new DataBase();
            User loggedUser = (User)Session["loggedUser"];

            IndexVM model = new IndexVM();

            model.Tasks = context.Tasks.Where(t => t.AssigneeId == loggedUser.Id).ToList();
            model.Cars = context.Cars.ToList();

            return View(model);
        }

        public ActionResult Done(int id)
        {
            DataBase context = new DataBase();
            User loggedUser = (User)Session["loggedUser"];

            Task task = context.Tasks.Where(t => t.Id == id).FirstOrDefault();
            if (task == null)
            {
                return RedirectToAction("Index", "MyCars");
            }
            if (task.AssigneeId != loggedUser.Id)
            {
                return RedirectToAction("Index", "MyCars");
            }

            if (task.Status == 0)
            {
                task.Status++;
                context.Tasks.Update(task);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "MyCars");
        }
    }
}