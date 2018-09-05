using FreightTech.Data.DataTransferObject;
using FreightTech.Data.Repositories;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace FreightTech.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/driver")]
    public class DriverController : ApiController
    {
        private readonly Lazy<int> _userId = new Lazy<int>(() => ((ClaimsPrincipal)Thread.CurrentPrincipal).Claims.Where(c => c.Type == "UserId")
                  .Select(c => Convert.ToInt32(c.Value)).FirstOrDefault());
        //private int _userId1 = ((ClaimsPrincipal)Thread.CurrentPrincipal).Claims.Where(c => c.Type == "UserId")
        //           .Select(c => Convert.ToInt32(c.Value)).FirstOrDefault();

        IDriverRepository _repository;
        IAppLogger _appLogger;

        #region ctor
        public DriverController(IDriverRepository repository, IAppLogger appLogger)
        {
            _repository = repository;
            _appLogger = appLogger;
        }
        #endregion

        #region Get
        [Route("all")]
        public async Task<IHttpActionResult> Get(bool isAccepted)
        {
            var drivers = await _repository.GetAllDriversAsync(isAccepted);
            return Ok(drivers);
        }
        #endregion

        #region Get
        [Route("{driverId}")]
        public async Task<IHttpActionResult> Get(int driverId)
        {
            var driver = await _repository.Get(driverId);
            return Ok(driver);

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
            var driver = await _repository.GetDriverDetailAsync(model.Username);
            return Ok(driver);
        }
        #endregion

        #region PostCurrentLocation
        [HttpPost]
        [Route("location/current")]
        public async Task<IHttpActionResult> PostCurrentLocation([FromBody] DriverLocationDTO model)
        {
            if (model.DriverId == default(int) || string.IsNullOrWhiteSpace(model.Latitude) || string.IsNullOrWhiteSpace(model.Longitude))
            {
                return BadRequest();
            }
            var response = await  _repository.UpdateCurrentLocation(model);
            return Ok(response);
        }
        #endregion

        #region GetCurrentLocation
        [HttpGet]
        [Route("{driverId}/location/current")]
        public async Task<IHttpActionResult> GetCurrentLocation(int driverId)
        {
            //    //TO REMOVE
            //    return GetFakeLocation(driverId);

            if (driverId == default(int))
            {
                return BadRequest();
            }
            var model = await _repository.GetCurrentLocation(driverId);
            return Ok(model);
        }
        #endregion

        #region PostApproveDriver
        [HttpPost]
        [Route("{driverId}/approve")]
        public async Task<IHttpActionResult> PostApproveDriver(int driverId)
        {
            // to approve driver call
            // /api/driver/123/approve

            var response = await _repository.ApproveDriver(driverId);
            return Ok(response);
        }
        #endregion

        //#region TO REMOVE

        //private IHttpActionResult GetFakeLocation(int driverId)
        //{
        //    if (driverId >= (array.Length / 2))
        //    {
        //        driverId--;
        //        return Ok(new DriverLocationDTO
        //        {
        //            DriverId = driverId,
        //            Latitude = array[driverId, 0],
        //            Longitude = array[driverId, 1]
        //        });
        //    }

        //    return Ok(new DriverLocationDTO
        //    {
        //        DriverId = driverId + 1,
        //        Latitude = array[driverId, 0],
        //        Longitude = array[driverId, 1]
        //    });
        //}

        //double[,] array = new double[,] { {24.9191437, 67.1020151},
        //    {24.919601, 67.101940},
        //    {24.919912, 67.101672},
        //    {24.920525, 67.101135},
        //    {24.920370, 67.100803},
        //    {24.920185, 67.100481},
        //    {24.919786, 67.099987},
        //    {24.919513, 67.099633},
        //    {24.919314, 67.099370},
        //    {24.919046, 67.099032},
        //    {24.918910, 67.098850},
        //    {24.918983, 67.098931},
        //    {24.918735, 67.098582},
        //    {24.918633, 67.098491}}; 
        //#endregion
    }

    public class DummyModel
    {
        public string Username { get; set; }
    }
}
