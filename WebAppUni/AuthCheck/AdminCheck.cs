﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppUni.Entities;

namespace WebAppUni.AuthCheck
{
    public class AdminCheck: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            User loggedUser = (User)context.HttpContext.Session["loggedUser"];
            if (loggedUser.Role != User.PermissionLevel.Admin)
            {
                context.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}