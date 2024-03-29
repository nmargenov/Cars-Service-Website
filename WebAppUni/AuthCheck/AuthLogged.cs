using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppUni.AuthCheck
{
    public class AuthLogged : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session["loggedUser"] != null)
            {
                context.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}