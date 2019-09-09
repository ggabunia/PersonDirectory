using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonDirectory.HelperClasses;

namespace PersonDirectory.Filters
{
    public class ExceptionHandlerController: Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            var ex = filterContext.Exception;
            ErrorLogService.LogError(ex);           
            filterContext.Result = RedirectToAction("Error", "Home");

 
        }
    }
}