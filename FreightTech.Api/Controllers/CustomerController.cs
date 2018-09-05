using FreightTech.Data.DataTransferObject;
using FreightTech.Data.Repositories;
using System.Threading.Tasks;
using System.Web.Http;

namespace FreightTech.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        #region Get
        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        [Route("all")]
        public async Task<IHttpActionResult> Get()
        {
            var customers = await _repository.GetAllCustomersAsync();
            return Ok(customers);
        } 
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{driverId}")]
        public async Task<IHttpActionResult> Delete(int driverId)
        {
            var status = await _repository.Delete(driverId);
            return Ok(status);

        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]DummyModel model)
        {
            var customer = await _repository.GetCustomerDetailAsync(model.Username);
            return Ok(customer);
        }
        #endregion

        #region Postfeedback
        [HttpPost]
        [Route("feedback")]
        public async Task<IHttpActionResult> Postfeedback([FromBody] FeedbackDTO model)
        {
            if (model.UserId == default(int) || string.IsNullOrWhiteSpace(model.Feedbacks))
            {
                return BadRequest();
            }
            var response = await _repository.SaveFeedback(model);
            return Ok(response);
        }
        #endregion

        #region GetAllFeedbacks
        [Route("feedback/all")]
        public async Task<IHttpActionResult> GetAllFeedbacks()
        {
            var feedbacks = await _repository.GetAllFeedbacks();
            return Ok(feedbacks);
        }
        #endregion

        #region GetAllOrders
        [Route("orders/all")]
        public async Task<IHttpActionResult> GetAllOrders()
        {
            var orders = await _repository.GetAllOrders();
            return Ok(orders);
        } 
        #endregion

    }
}
