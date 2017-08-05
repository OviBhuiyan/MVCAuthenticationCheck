using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCAuthenticationCheck.Controllers
{
    public class BASEController : Controller
    {
        private string ActionKey;

        //sample data for the roles of the application
        Dictionary<string, List<string>> AllRoles =
                   new Dictionary<string, List<string>>();

        protected void initRoles()
        {
            AllRoles.Add("role1", new List<string>() { "Home-Index",
      "Controller1-Create", "Controller1-Edit", "Controller1-Delete" });
            AllRoles.Add("role2", new List<string>() { "Home-About", "Controller1-Create" });
            AllRoles.Add("role3", new List<string>() { "Home-Contact" });
        }

        // roles = "admin;customer"; 
        //sample data for the pages that need authorization
        List<string> NeedAuthenticationActions =
          new List<string>() { "Home-About", "Home-Index", "Home-Contact", "Controller1-Delete" };


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            initRoles();
            ActionKey = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                               "-" + filterContext.ActionDescriptor.ActionName;

            string role = "role1;role2";//Session["Roles"].ToString();//getting the current role- multiple row
           

            if (NeedAuthenticationActions.Any(s => s.Equals(ActionKey, StringComparison.OrdinalIgnoreCase)))
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    string redirectUrl = string.Format("?returnUrl={0}",
                            filterContext.HttpContext.Request.Url.PathAndQuery);
                    filterContext.HttpContext.Response.Redirect(FormsAuthentication.LoginUrl + redirectUrl, true);
                }
                else //check role
                {
                    string[] roleList = role.Split(';');
                    List<string> roleListAllDetails = new List<string>();
                    foreach (string rol in roleList)
                    {
                        roleListAllDetails.AddRange(AllRoles[rol].ToList());
                    }
                    if (!roleListAllDetails.Contains(ActionKey))
                    {
                        filterContext.HttpContext.Response.Redirect("~/NoAccess", true);
                    }
                }
            }

            //Else Means Allowanonymus
        }
    }
}

//1. Check which Controller And Action Need Authentication ( If True - need to check Authentication or anonymous)
//2. for true = If now you are not logged in then Redirect The log in page;
// false = else- if your


//Reference - https://www.codeproject.com/Tips/620420/MVC-Dynamic-Authorization