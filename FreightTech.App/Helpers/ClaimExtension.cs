using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace FreightTech.App.Helpers
{
    public class ClaimExtension
    {
        public ClaimExtension()
        {

        }
        public List<Claim> AddClaims(string emailId, string userId, string name, string role, string userImage = "")
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, emailId));
            claims.Add(new Claim("userId", userId));
            claims.Add(new Claim(ClaimTypes.Name, name));
            claims.Add(new Claim(ClaimTypes.Role, role));
            claims.Add(new Claim(ClaimTypes.Uri, userImage));
            //claims.Add(new Claim(ClaimTypes.Sid, "123"));
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Set current principal
            Thread.CurrentPrincipal = claimsPrincipal;
            return claims;
        }

        ////Get the current claims principal
        //var identityy = (ClaimsPrincipal)Thread.CurrentPrincipal;
        //// Get the claims values
        //var name = identityy.Claims.Where(c => c.Type == ClaimTypes.Name)
        //                   .Select(c => c.Value).SingleOrDefault();
        //var sid = identityy.Claims.Where(c => c.Type == ClaimTypes.Sid)
        //                   .Select(c => c.Value).SingleOrDefault();
    }

    public class FreightTechIdentity : ClaimsIdentity
    {
        public const string RolesClaimType = "http://www.assilabdulrahim.com/CuttingEdge.Security.Role";
        public const string GroupClaimType = "http://www.assilabdulrahim.com/CuttingEdge.Security.Group";
        public const string IPClaimType = "http://www.assilabdulrahim.com/CuttingEdge.Security.IP";
        public const string IdClaimType = "http://www.assilabdulrahim.com/CuttingEdge.Security.Id";


        public FreightTechIdentity(IEnumerable<Claim> claims, string authenticationType)
            : base(claims, authenticationType: authenticationType)
        {

            AddClaims(from @group in claims where @group.Type == GroupClaimType select @group);
            AddClaims(from role in claims where role.Type == RoleClaimType select role);
            AddClaims(from id in claims where id.Type == IdClaimType select id);
            AddClaims(from ip in claims where ip.Type == IPClaimType select ip);
        }

        public FreightTechIdentity(IEnumerable<string> roles, IEnumerable<string> groups, string IP, int id)
        {
            AddClaims(from @group in groups select new Claim(GroupClaimType, @group));
            AddClaims(from role in roles select new Claim(RolesClaimType, role));
            AddClaim(new Claim(IdClaimType, id.ToString()));
            AddClaim(new Claim(IPClaimType, IP.ToString()));
        }

        public IEnumerable<string> Roles {
            get {
                return from claim in FindAll(RolesClaimType) select claim.Value;
            }
        }

        public IEnumerable<string> Groups { get { return from claim in FindAll(GroupClaimType) select claim.Value; } }

        public int Id { get { return Convert.ToInt32(FindFirst(IdClaimType).Value); } }
        public string IP { get { return FindFirst(IPClaimType).Value; } }
    }

    public class FreightTechPrincipal : ClaimsPrincipal
    {
        public FreightTechPrincipal(FreightTechIdentity identity)
            : base(identity)
        {

        }

        public FreightTechPrincipal(ClaimsPrincipal claimsPrincipal)
            : base(claimsPrincipal)
        {

        }
    }
}