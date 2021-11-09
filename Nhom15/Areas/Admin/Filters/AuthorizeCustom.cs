using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom15.Areas.Admin.Filters
{
    public class AuthorizeCustom : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new HttpUnauthorizedResult();
            if (HttpContext.Current.Session["Username"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Home/Index");
            }
        }
    }
}