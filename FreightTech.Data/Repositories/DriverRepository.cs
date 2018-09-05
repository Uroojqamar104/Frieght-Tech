using FreightTech.Data.DataTransferObject;
using FreightTech.Data.User;
using FreightTech.Enum;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FreightTech.Data.Converters;

namespace FreightTech.Data.Repositories
{
    public interface IDriverRepository
    {
        Task<DriverDetail> GetDriverDetailAsync(int driverId);
        Task<object> GetAllDriversAsync(bool isAccepted);
        Task<UserProfile> Get(int driverId);
        Task<UserProfile> GetDriverDetailAsync(string userName);
        Task<bool> UpdateCurrentLocation(DriverLocationDTO model);
        Task<DriverLocationDTO> GetCurrentLocation(int driverId);
        Task<object> ApproveDriver(int driverId);
        Task<ResponseModel> Delete(int driverId);
        Task<OrderDTO> GetActiveOrder(int driverId);
    }

    public class DriverRepository : UserRepository, IDriverRepository, IDisposable
    {
        FreightTechContext context = new FreightTechContext();

        public DriverRepository()
        {
        }

        public async Task<DriverDetail> GetDriverDetailAsync(int driverId)
        {
            return await context.DriverDetail.FirstOrDefaultAsync(a => a.DriverId == driverId);
        }

        //public async Task<object> GetDriverDetailAsync(int driverId)
        //{
        //    var response = await (from driver in context.UserProfile
        //                         join detail in context.DriverDetail on driver.UserId equals detail.DriverId
        //                         where driver.RoleId == (int)UserRole.Driver && driver.UserId == driverId
        //                         select new { driver, detail }).FirstOrDefaultAsync();
        //    return response;
        //}

        public async Task<UserProfile> GetDriverDetailAsync(string userName)
        {
            return await context.UserProfile
                .FirstOrDefaultAsync(a => a.RoleId == (int)UserRole.Driver 
                && a.EmailId.ToLower().Trim().Equals(userName.ToLower().Trim()) 
                && a.RowState != (int)RowState.Deleted);
        }

        public async Task<object> GetAllDriversAsync(bool isAccepted)
        {
            var drivers = await (from driver in context.UserProfile
                                 join detail in context.DriverDetail on driver.UserId equals detail.DriverId
                                 where driver.RoleId == (int)UserRole.Driver 
                                 && detail.IsAccepted == isAccepted
                                 &&  driver.RowState != (int)RowState.Deleted
                                 select new { driver, detail }).ToListAsync();
            return drivers;
        }

        public async Task<UserProfile> Get(int driverId)
        {
            var driver = await context.UserProfile.FirstOrDefaultAsync(a => a.RoleId == (int)UserRole.Driver && a.UserId == driverId && a.RowState != (int)RowState.Deleted);
            return driver;
        }

        public async Task<bool> UpdateCurrentLocation(DriverLocationDTO model)
        {
            var driver = context.DriverDetail.FirstOrDefault(a => a.DriverId == model.DriverId);
            if (ReferenceEquals(driver, null))
            {
                return false;
            }

            driver.CurrentLatitude = model.Latitude;
            driver.CurrentLongitude = model.Longitude;
            driver.LastLocationUpdated = DateTime.Now;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<DriverLocationDTO> GetCurrentLocation(int driverId)
        {
            var driver = await GetDriverDetailAsync(driverId);
            if (ReferenceEquals(driver, null) || string.IsNullOrWhiteSpace(driver.CurrentLatitude) || string.IsNullOrWhiteSpace(driver.CurrentLongitude))
            {
                return null;
            }

            var model = new DriverLocationDTO
            {
                DriverId = driverId,
                Latitude = driver.CurrentLatitude,
                Longitude = driver.CurrentLongitude
            };

            return model;
        }

        public async Task<object> ApproveDriver(int driverId)
        {
            var driver = await GetDriverDetailAsync(driverId);
            if (driver == null)
            {
                return new { status = false, message = "Driver not found." };
            }

            driver.IsAccepted = true;
            await context.SaveChangesAsync();

            return new { status = true, message = "" };
        }

        public new async Task<ResponseModel> Delete(int driverId)
        {
            var response = new ResponseModel();

            var currentActiveOrder = await GetActiveOrder(driverId);
            if (currentActiveOrder != null && currentActiveOrder.OrderId != default (int))
            {
                response.errors.Add("Driver with a pending order cannot be deleted.");
                return response;
            }

            response = await base.Delete(driverId);
            return response;
        }

        public async Task<OrderDTO> GetActiveOrder(int driverId)
        {
            var order = await context.Order                
                .FirstOrDefaultAsync(a => a.DriverId == driverId && a.StatusId == (int)OrderStatus.Pending);
            return order.ToDtoEntity();
        }

        
    }

    
}
