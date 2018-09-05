using FreightTech.Data.DataTransferObject;
using FreightTech.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FreightTech.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<ResponseModel> SaveNewOrder(Order order);
        Task<object> GetNewOrders(int driverId);
        Task<object> GetAllOrders(int statusId);
    }

    public class OrderRepository : IOrderRepository, IDisposable
    {
        FreightTechContext context = new FreightTechContext();

        public OrderRepository()
        {

        }

        #region SaveNewOrder
        public async Task<ResponseModel> SaveNewOrder(Order order)
        {
            var response = new ResponseModel();
            #region Validate
            if (order.CustomerId == default(int))
            {
                response.errors.Add("Customer id is required.");
            }
            if (order.StatusId == default(int))
            {
                response.errors.Add("Status id is required.");
            }
            if (string.IsNullOrWhiteSpace(order.PickupLocation))
            {
                response.errors.Add("Pick up location is required.");
            }
            if (string.IsNullOrWhiteSpace(order.DropoffLocation))
            {
                response.errors.Add("Drop off location is required.");
            }
            if (string.IsNullOrWhiteSpace(order.PickupLatitude) || string.IsNullOrWhiteSpace(order.PickupLongitude))
            {
                response.errors.Add("Pick up latitude/longitude is required.");
            }
            if (string.IsNullOrWhiteSpace(order.DropoffLatitude) || string.IsNullOrWhiteSpace(order.DropoffLongitude))
            {
                response.errors.Add("Drop off latitude/longitude is required.");
            }
            if (string.IsNullOrWhiteSpace(order.LoadType))
            {
                response.errors.Add("Load type is required.");
            }
            if (string.IsNullOrWhiteSpace(order.Commodity))
            {
                response.errors.Add("Commodity is required.");
            }
            if (order.Weight == default(decimal))
            {
                response.errors.Add("Weight is required.");
            }
            if (string.IsNullOrWhiteSpace(order.ContactNumber))
            {
                response.errors.Add("Contact Number is required.");
            }
            #endregion
            if (response.errors.Count > 0)
            {
                return response;
            }

            order.DateCreated = DateTime.Now;
            context.Order.Add(order);
            await context.SaveChangesAsync();
            response.status = true;
            return response;
        } 
        #endregion

        #region GetNewOrders
        public async Task<object> GetNewOrders(int driverId)
        {
            var response = new ResponseModel();

            if (driverId == default(int))
            {
                response.errors.Add("Driver id is required.");
                return response;
            }

            var data = context.DriverDetail.FirstOrDefault(a => a.DriverId == driverId && a.IsLoaded);
            if (data != null)
            {
                response.errors.Add("Sorry your vehicle is already loaded.");
                return response;
            }

            var orders = await (from customer in context.UserProfile
                                join order in context.Order on customer.UserId equals order.CustomerId
                                where order.StatusId == (int)OrderStatus.New
                                select new { customer, order }).ToListAsync();

            response.status = true;
            response.response = orders;
            return response;
        }
        #endregion

        public async Task<object> GetAllOrders(int statusId)
        {
            var orders = await (from order in context.Order
                                join customer in context.UserProfile on order.CustomerId equals customer.UserId
                                join driver in context.UserProfile on order.DriverId equals driver.UserId
                                join driverDetails in context.DriverDetail on driver.UserId equals driverDetails.DriverId
                                where order.StatusId == (statusId == default(int) ? order.StatusId : statusId)
                                && order.DriverId != null
                                select new { order, customer, driver, driverDetails }).ToListAsync();
            return orders;
        }

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
