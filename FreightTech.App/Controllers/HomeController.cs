using FreightTech.Data;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;

namespace FreightTech.App.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private string role = ((ClaimsPrincipal)Thread.CurrentPrincipal).Claims.Where(c => c.Type == ClaimTypes.Role)
                   .Select(c => c.Value).FirstOrDefault();
        private int userId = ((ClaimsPrincipal)Thread.CurrentPrincipal).Claims.Where(c => c.Type == "userId")
                   .Select(c => Convert.ToInt32(c.Value)).FirstOrDefault();
        private string userName = ((ClaimsPrincipal)Thread.CurrentPrincipal).Claims.Where(c => c.Type == ClaimTypes.Name)
                   .Select(c => c.Value).FirstOrDefault();
        //claims.Add(new Claim(ClaimTypes.Name, name));
        FreightTechContext context;

        public HomeController()
        {
            context = new FreightTechContext();
        }


        public ActionResult Index()
        {
            ViewBag.Title = "Dashboard";
            ViewBag.LoggedInUserName = userName;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Test()
        {
            ViewBag.Title = "Test";
            return View();
        }

        [AllowAnonymous]
        public ActionResult Tracking()
        {
            return View();
        }

    }
}