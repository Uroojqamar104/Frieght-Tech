﻿using FreightTech.Data;
using System;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebMatrix.WebData;

namespace FreightTech.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,
   AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized
            (ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                System.Data.Entity.Database.SetInitializer<FreightTechContext>(null);

                try
                {
                    using (var context = new FreightTechContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework 
                            // migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    //WebSecurity.InitializeDatabaseConnection
                    //("DefaultConnection",
                    //"UserProfile", "UserId",
                    //"UserName", autoCreateTables: true);
                    WebSecurity.InitializeDatabaseConnection
                    ("FreightTechConnection",
                    "UserProfile", "UserId",
                    "EmailId", autoCreateTables: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership" +
                    "database could not be initialized. For more information," +
                    "please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}