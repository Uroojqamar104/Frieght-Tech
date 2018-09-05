using FreightTech.Api.Filters;
using FreightTech.Data;
using FreightTech.Data.Repositories;
using FreightTech.Enum;
using FreightTech.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;
using System.Linq;

namespace FreightTech.Api.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        UserRepository GetUserRepository()
        {
            return UserRepository.Instance;
        }

        FreightTechContext context = new FreightTechContext();


        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [InitializeSimpleMembershipAttribute]
        public async Task<IHttpActionResult> Register([FromBody] RegisterModel model)
        {
            var errors = new List<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.RoleId == (int)UserRole.Driver)
                    {
                        if (string.IsNullOrWhiteSpace(model.Vehicle))
                        {
                            errors.Add("Vehicle name is required");
                        }
                        if (model.Contact.Length < 11)
                        {
                            errors.Add("Contact is required");
                        }
                    }
                    if (model.RoleId == (int)UserRole.Customer)
                    {

                    }

                    if (string.IsNullOrWhiteSpace(model.FirstName))
                    {
                        errors.Add("First name is required");
                    }
                    if (string.IsNullOrWhiteSpace(model.LastName))
                    {
                        errors.Add("Last name is required");
                    }
                    if (string.IsNullOrWhiteSpace(model.Email))
                    {
                        errors.Add("EmailId is required");
                    }

                    if (errors.Count > 0)
                    {
                        return Json(new { status = errors.Count == 0, errors });
                    }

                    WebSecurity.CreateUserAndAccount(model.Email,
                        new Security(model.Password).Encrypt(),
                        propertyValues: new
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,

                            RoleId = model.RoleId,
                            RowState = (int)RowState.Created,
                            HasImage = false,
                        });

                    int driverId = context.UserProfile.First(a => a.EmailId == model.Email).UserId;
                    if (model.RoleId == (int)UserRole.Customer)
                    {

                    }
                    else if (model.RoleId == (int)UserRole.Driver)
                    {
                        var driver = new DriverDetail();
                        driver.DriverId = driverId;
                        driver.VehicleNumber = model.Vehicle;
                        driver.Contact = model.Contact;
                        driver.StatusId = (int)DriverStatus.LoggedOut;
                        driver.IsLoaded = false;
                        driver.IsAccepted = false;
                        driver.CurrentLatitude = "0.0";
                        driver.CurrentLongitude = "0.0";


                        context.DriverDetail.Add(driver);
                        try
                        {
                            await context.SaveChangesAsync();
                        }
                        catch (System.Exception ex)
                        {

                            throw;
                        }
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    errors.Add(e.StatusCode.ToString());
                }
            }
            return Json(new { status = errors.Count == 0, errors });
        }




    }






    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }

        public string Vehicle { get; set; }
        public string Contact { get; set; }

        public string Name {
            get {
                return FirstName + LastName;
            }
            set {
                //FirstName = value;
            }
        }


        //public string NIC { get; set; }
        //public string CellPhone { get; set; }
        //public string HomePhone { get; set; }
    }
}

//{
//	"Email": "customer@gmail.com",
//	"Password": "abc123",
//	"FirstName": "Customer",
//	"LastName": "One",
//	"RoleId": 3,
//	"Vehicle": "abc",
//	"Contact": 03333333333
//}