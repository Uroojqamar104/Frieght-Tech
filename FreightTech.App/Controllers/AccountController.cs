using FreightTech.App.Helpers;
using FreightTech.App.Models;
using FreightTech.Data.Repositories;
using FreightTech.Enum;
using FreightTech.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace FreightTech.App.Controllers
{
    public class AccountController : Controller
    {
        UserRepository GetUserRepository()
        {
            return UserRepository.Instance;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model, string returnUrl)
        {
            var errors = new List<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    var token = WebSecurity.CreateUserAndAccount(model.Email,
                        new Security(model.Password).Encrypt(),
                        propertyValues: new
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            RoleId = (int)UserRole.SuperAdmin,
                            RowState = (int)RowState.Created,
                            HasImage = false,
                        });

                    if (!string.IsNullOrWhiteSpace(token))
                    {

                    }
                    //var profile = new UserProfile
                    //{
                    //    EmailId = model.email,
                    //    IsActive = true,
                    //    UserRoleId = 1
                    //};
                    //context.UserProfile.Add(profile);
                    //context.SaveChanges();
                }
                catch (MembershipCreateUserException e)
                {
                    errors.Add(e.StatusCode.ToString());
                }
            }
            return Json(new { status = errors.Count == 0, errors });
        }

        [HttpPost]
        [AllowAnonymous]
        //  [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var error = "";
            ApiToken tokenData = null;
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(model.Email, new Security(model.Password).Encrypt(), persistCookie: false)) //Check the database
                {
                    var user = GetUserRepository().GetUserProfile(model.Email);
                    string role = ((UserRole)user.RoleId).ToString();

                    string baseUrl = Request.Url.AbsoluteUri.ToLowerInvariant().Contains("localhost") ? AppKeys.LocalApiDomain : AppKeys.LocalApiDomain;
                    
                    tokenData = await WebHelper.GetTokenAsync(model.Email, model.Password, baseUrl);

                    List<Claim> claims = new ClaimExtension().AddClaims(model.Email, user.UserId.ToString(), user.FirstName + " " + user.LastName, role);
                    if (null != claims)
                    {
                        AddClaims(claims);
                    }
                }
                else
                    error = ("Invalid username or password!.");
            }
            else
                error = ("Data is not valid!.");
            return Json(new { status = error == "", error, tokenData });
        }

        #region Logout
        //[HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            //Session.Clear();
            AuthenticationManager.SignOut();
            WebSecurity.Logout();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        private void AddClaims(List<Claim> claims)
        {
            var claimsIdentity = new FreightTechIdentity(claims,
            DefaultAuthenticationTypes.ApplicationCookie);
            //This uses OWIN authentication        
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, claimsIdentity);
            HttpContext.User = new FreightTechPrincipal(AuthenticationManager.AuthenticationResponseGrant.Principal);
        }

        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult TestNoti()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SendNoti(string token, string message)
        {
            //string to = "fm3PMXAGaW8:APA91bEfDymyYJg5VUw7pKvMqsvBDHl_gJ5-jrV--Vvsojk8M9Svy5gZYJUol0fieqkIWqF3I2A2RZLeyGI5Y-w_16n7qN9WzI849ND98-VDV4bW0JOhuTuDLrdwp5QuiXKtNmvYfOUb";
            string to = token;
            string title = "This is my title";
            //string message = "Hi hi hi hih i";
            var abc = new GoogleFirebaseNotification().QueueMessage(to, title, message, "http://google.com");
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}