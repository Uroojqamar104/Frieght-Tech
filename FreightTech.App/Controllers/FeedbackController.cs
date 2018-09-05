using System.Web.Mvc;

namespace FreightTech.App.Controllers
{
    public class FeedbackController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
