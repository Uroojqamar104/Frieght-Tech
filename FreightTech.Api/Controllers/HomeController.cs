using FreightTech.Data;
using FreightTech.Enum;
using System.Linq;
using System.Web.Http;

namespace FreightTech.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        FreightTechContext context = new FreightTechContext();

        [Route("get/stats")]
        public IHttpActionResult GetStats()
        {
            var pendingOrdersCount = context.Order.Count(a => a.StatusId == (int)OrderStatus.Pending);
            var completedOrdersCount = context.Order.Count(a => a.StatusId == (int)OrderStatus.Done);
            var pendingDriversCount = context.DriverDetail.Count(a => !a.IsAccepted);
            var totalFeedbackCount = context.Feedback.Count();


            return Ok(new { pendingOrdersCount, completedOrdersCount, pendingDriversCount, totalFeedbackCount });
        }
    }
}
checked;