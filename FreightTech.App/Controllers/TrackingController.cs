using System.Web.Mvc;

namespace FreightTech.App.Controllers
{
    [Authorize]
    public class TrackingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Orders()
        {            
            return View();
        }
    }
}