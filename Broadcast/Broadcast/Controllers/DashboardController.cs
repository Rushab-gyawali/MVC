using Broadcast.Business.Business;
using Broadcast.Shared.Common;
using Broadcast.Web.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Broadcast.Controllers
{
    public class DashboardController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }
    }
}