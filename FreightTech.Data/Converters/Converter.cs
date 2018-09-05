using FreightTech.Data.DataTransferObject;
using FreightTech.Enum;

namespace FreightTech.Data.Converters
{
    public static class Converter
    {
        public static OrderDTO ToDtoEntity(this Order order)
        {
            if (order == null)
            {
                return new OrderDTO();
            }
            return new OrderDTO
            {
                OrderId = order.OrderId,
                StatusId = order.StatusId,
                Status = ((OrderStatus)order.StatusId),
                PickupLocation = order.PickupLocation,
                DropoffLocation = order.DropoffLocation,
                PickupLatitude = order.PickupLatitude,
                PickupLongitude = order.PickupLongitude,
                DropoffLatitude = order.DropoffLatitude,
                DropoffLongitude = order.DropoffLongitude,

                LoadType = order.LoadType,
                Commodity = order.Commodity,
                Weight = order.Weight,
                ContactNumber = order.ContactNumber,
                DateCreated = order.DateCreated,

                //Customer = 
                //Driver = 
            };

        }
    }
}
