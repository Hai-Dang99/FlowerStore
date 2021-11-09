using Nhom15.Areas.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom15.Areas.Admin.Controllers
{
    [AuthorizeCustom]
    public class BaseController : Controller
    {
        
    }
}