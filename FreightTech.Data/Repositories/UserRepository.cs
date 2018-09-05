using FreightTech.Data.DataTransferObject;
using FreightTech.Data.User;
using FreightTech.Enum;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FreightTech.Data.Repositories
{
    public interface IUserRepository
    {
        UserProfile GetUserProfile(string emailId);
        Task<UserProfile> GetUserProfile(int userId);
        Task<ResponseModel> Delete(int userId);
        // Task<List<UserProfile>> GetAllUsersByType(int typeId);
    }

    public class UserRepository : IUserRepository, IDisposable
    {
        FreightTechContext context = new FreightTechContext();

        public UserRepository()
        {
            //context = FreightTechContext.Instance;
        }

        private static UserRepository instance;

        public static UserRepository Instance {
            get {
                if (instance == null)
                {
                    instance = new UserRepository();
                }
                return instance;
            }
        }

        public UserProfile GetUserProfile(string emailId)
        {
            var user = context.UserProfile.FirstOrDefault(a => a.EmailId == emailId && a.RowState != (int)RowState.Deleted);
            if (user.RoleId == (int)UserRole.Driver)
            {
                var driverDetail = context.DriverDetail.FirstOrDefault(a => a.DriverId == user.UserId);
                if (driverDetail == null || !driverDetail.IsAccepted)
                {
                    return null;
                }
            }
            return user;
        }

        public async Task<UserProfile> GetUserProfile(int userId)
        {
            return await context.UserProfile.FirstOrDefaultAsync(a => a.UserId == userId && a.RowState != (int)RowState.Deleted);
        }

        #region Delete
        public async Task<ResponseModel> Delete(int userId)
        {
            var response = new ResponseModel();
            var user = await context.UserProfile.FirstOrDefaultAsync(a => a.UserId == userId);
            if (user == null)
            {
                response.errors.Add("Invalid user id.");
                return response;
            }

            user.RowState = (int)RowState.Deleted;
            await context.SaveChangesAsync();
            response.status = true;
            return response;
        } 
        #endregion


        #region Dispose
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}

//public Task<List<UserProfile>> GetAllAsync()
//{
//    return ((ICustomerRepository)instance).GetAllAsync();
//}

//public Task<UserProfile> GetCustomerDetailAsync(string userName)
//{
//    return ((ICustomerRepository)instance).GetCustomerDetailAsync(userName);
//}
//public UserProfile Get(int userId)
//{
//    var context = new FreightTechContext();
//    return context.UserProfile.FirstOrDefault(a => a.UserId == userId);
//}
