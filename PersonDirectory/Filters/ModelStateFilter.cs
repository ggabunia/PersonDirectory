using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PersonDirectory.Filters
{
    public class ModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
               
                List<string> modelErrors = new List<string>();
                foreach (var modelState in filterContext.Controller.ViewData.ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        if (!String.IsNullOrWhiteSpace(modelError.ErrorMessage))
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                        
                    }
                }
                if (modelErrors.Any())
                {
                     string errorString = "";
                    foreach(var item in modelErrors)
                    {
                        errorString += item + ";\n";
                    }
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, errorString);
                }
               
            }
        }
    }
}
