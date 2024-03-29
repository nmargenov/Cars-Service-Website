using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppUni.Entities;

namespace WebAppUni.AuthCheck
{
    public class AuthNotLogged : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session["loggedUser"] == null)
            {
                context.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}