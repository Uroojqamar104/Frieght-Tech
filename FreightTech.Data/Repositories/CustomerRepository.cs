using FreightTech.Data.Converters;
using FreightTech.Data.DataTransferObject;
using FreightTech.Data.User;
using FreightTech.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FreightTech.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<UserProfile>> GetAllCustomersAsync();
        Task<UserProfile> GetCustomerDetailAsync(string userName);
        Task<bool> SaveFeedback(FeedbackDTO model);
        Task<object> GetAllFeedbacks();
        Task<object> GetAllOrders();
        Task<ResponseModel> Delete(int customerId);
        Task<OrderDTO> GetActiveOrder(int customerId);
    }

    public class CustomerRepository : UserRepository, ICustomerRepository, IDisposable
    {
        FreightTechContext context = new FreightTechContext();

        public CustomerRepository()
        {
        }

        #region GetAllCustomersAsync
        public async Task<List<UserProfile>> GetAllCustomersAsync()
        {
            var customers = context.UserProfile.Where(a => a.RoleId == (int)UserRole.Customer && a.RowState != (int)RowState.Deleted);
            return await customers.ToListAsync();
        }
        #endregion

        #region GetCustomerDetailAsync
        public async Task<UserProfile> GetCustomerDetailAsync(string userName)
        {
            return await context.UserProfile
                .FirstOrDefaultAsync(a => a.RoleId == (int)UserRole.Customer
            && a.EmailId.ToLower().Trim().Equals(userName.ToLower().Trim())
            && a.RowState != (int)RowState.Deleted);
        }
        #endregion

        #region SaveFeedback
        public async Task<bool> SaveFeedback(FeedbackDTO model)
        {
            var user = GetUserProfile(model.UserId);
            if (ReferenceEquals(user, null))
            {
                return false;
            }
            var feedback = new Feedback
            {
                UserId = model.UserId,
                Feedbacks = model.Feedbacks
            };

            context.Feedback.Add(feedback);

            await context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region GetAllFeedbacks
        public async Task<object> GetAllFeedbacks()
        {
            var feedbacks = await (from user in context.UserProfile
                                   join detail in context.Feedback on user.UserId equals detail.UserId
                                   select new { user, detail }).ToListAsync();
            return feedbacks;
        }
        #endregion

        #region GetAllOrders
        public async Task<object> GetAllOrders()
        {
            var feedbacks = await (from user in context.UserProfile
                                   join detail in context.Order on user.UserId equals detail.CustomerId
                                   select new { user, detail }).ToListAsync();
            return feedbacks;
        }
        #endregion

        #region Delete
        public new async Task<ResponseModel> Delete(int customerId)
        {
            var response = new ResponseModel();

            var currentActiveOrder = await GetActiveOrder(customerId);
            if (currentActiveOrder != null && currentActiveOrder.OrderId != default(int))
            {
                response.errors.Add("Customer with a pending order cannot be deleted.");
                return response;
            }

            response = await base.Delete(customerId);
            return response;
        }
        #endregion

        #region GetActiveOrder
        public async Task<OrderDTO> GetActiveOrder(int customerId)
        {
            var order = await context.Order
                .FirstOrDefaultAsync(a => a.CustomerId == customerId && a.StatusId == (int)OrderStatus.Pending);
            return order.ToDtoEntity();
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
