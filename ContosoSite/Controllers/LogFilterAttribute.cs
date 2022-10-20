using ContosoSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoSite.Controllers
{
    public class LogFilterAttribute
    {
    }

    public class CachingFilterAttribute : ActionFilterAttribute
    {

        public void ActionLog(string cName, string AR, string ClientIPAddr, DateTime ReqDateTime, string ActionType)
        {
            using (ContosoUniversityDataEntities cuDb = new ContosoUniversityDataEntities())
            {
                ActionLog log = new ActionLog()
                {
                    ControllerName = cName,
                    ActionRequested = string.Concat(AR, " (Logged By: On" + ActionType + ")"),
                    ClientIPAddress = ClientIPAddr,
                    RequestedDateTime = ReqDateTime
                };
                cuDb.ActionLogs.Add(log);
                cuDb.SaveChanges();
            }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //filterContext.HttpContext.Response.Cookies.Clear();
            ActionLog(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName, filterContext.HttpContext.Request.UserHostAddress,
                    filterContext.HttpContext.Timestamp, "ActionExecuting");

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ActionLog(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName, filterContext.HttpContext.Request.UserHostAddress,
                    filterContext.HttpContext.Timestamp, "ActionExecuted");

        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            ActionLog(filterContext.RouteData.Values["controller"].ToString(),
                    filterContext.RouteData.Values["action"].ToString(), filterContext.HttpContext.Request.UserHostAddress,
                    filterContext.HttpContext.Timestamp, "ResultExecuting");

        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            ActionLog(filterContext.RouteData.Values["controller"].ToString(),
                    filterContext.RouteData.Values["action"].ToString(), filterContext.HttpContext.Request.UserHostAddress,
                    filterContext.HttpContext.Timestamp, "ResultExecuted");

        }
    }
}