using FreightTech.Api.Helpers;
using FreightTech.Data;
using FreightTech.Data.Repositories;
using System.Threading.Tasks;
using System.Web.Http;

namespace FreightTech.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        IOrderRepository _repository;
        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        #region Post
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Order order)
        {
            var response = await _repository.SaveNewOrder(order);
            if (response.status)
            {
                // notify drivers
                await new PushNotification().NotifyDriversAboutNewOrders();
            }
            return Ok(response);
        }
        #endregion

        #region GetNewOrders
        [HttpGet]
        [Route("new/{driverId}")]
        public async Task<IHttpActionResult> GetNewOrders(int driverId)
        {
            var orders = await _repository.GetNewOrders(driverId);
            return Ok(orders);
        }
        #endregion

        #region GetAllOrders
        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetOrders(int statusId)
        {
            var orders = await _repository.GetAllOrders(statusId);
            return Ok(orders);
        }
        #endregion

        #region TO REMOVE
        //[Authorize]
        //[Route("get2")]
        //public IHttpActionResult Get()
        //{
        //    return Ok(OrderTest.CreateOrders());
        //}

        //[Authorize]
        //[Route("post2")]
        //[HttpPost]
        //public IHttpActionResult Post2()
        //{
        //    return Ok(OrderTest.CreateOrders());
        //}

        //[AllowAnonymous]
        //[Route("get1")]
        //public IHttpActionResult Get1()
        //{
        //    return Ok(OrderTest.CreateOrders());
        //}

        //[AllowAnonymous]
        //[Route("select")]
        //public IHttpActionResult Select()
        //{
        //    return Ok(OrderTest.CreateOrders());
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("getorderid")]
        //public async Task<IHttpActionResult> GetOrderId()
        //{
        //    FreightTechContext context = new FreightTechContext();

        //    var value = context.Test.Max(p => p.OrderId) + 1;

        //    return Ok(value);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("test")]
        //public async Task<IHttpActionResult> PostTest([FromBody] TestModel model)
        //{
        //    FreightTechContext context = new FreightTechContext();
        //    var profile = new Test
        //    {
        //        Lat = model.Lat,
        //        Long = model.Long,
        //        OrderId = model.OrderId,
        //        DateCreated = DateTime.Now
        //    };
        //    context.Test.Add(profile);
        //    await context.SaveChangesAsync();

        //    return Ok();
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("getbyorderid")]
        //public async Task<IHttpActionResult> GetByOrderId([FromUri] int orderId)
        //{
        //    FreightTechContext context = new FreightTechContext();
        //    var trackings = await context.Test
        //        .Where(a => a.OrderId == orderId)
        //        .OrderBy(a => a.DateCreated)
        //        .Select(a => new { lat = a.Lat, lng = a.Long }).ToListAsync();

        //    return Ok(trackings);
        //}
        #endregion
    }

    //public class OrderTest
    //{
    //    public int OrderID { get; set; }
    //    public string CustomerName { get; set; }
    //    public string ShipperCity { get; set; }
    //    public Boolean IsShipped { get; set; }

    //    public static List<OrderTest> CreateOrders()
    //    {
    //        List<OrderTest> OrderList = new List<OrderTest>
    //        {
    //            new OrderTest {OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true },
    //            new OrderTest {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false},
    //            new OrderTest {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false },
    //            new OrderTest {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false},
    //            new OrderTest {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true}
    //        };

    //        return OrderList;

    //    }
    //}

    //public class TestModel
    //{
    //    public int TestId { get; set; }
    //    public int OrderId { get; set; }
    //    public string Lat { get; set; }
    //    public string Long { get; set; }
    //    public DateTime DateCreated { get; set; }
    //}
}
//ngrok authtoken 2eWKdu8RpDqn4UAfJ9Lty_2c2nvRMoYfa9U1Xnpj11k
//ngrok http -subdomain=freighttechapi 52219
//ngrok http 52219 -host-header="localhost:52219"



//https://www.raywenderlich.com/155-android-listview-tutorial-with-kotlin
//https://www.tutorialspoint.com/android/android_list_view.htm