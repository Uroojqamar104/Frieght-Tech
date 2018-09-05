using FreightTech.Enum;
using System;

namespace FreightTech.Data.DataTransferObject
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public CustomerDTO Customer { get; set; }
        public DriverDTO Driver { get; set; }
        public int StatusId { get; set; }
        public OrderStatus Status { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public string PickupLatitude { get; set; }
        public string PickupLongitude { get; set; }
        public string DropoffLatitude { get; set; }
        public string DropoffLongitude { get; set; }
        public string LoadType { get; set; }
        public string Commodity { get; set; }
        public decimal Weight { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
