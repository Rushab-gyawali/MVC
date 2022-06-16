using System.Web.Mvc;

namespace MVC.Controllers
{
    public class DashboardController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }
    }
}