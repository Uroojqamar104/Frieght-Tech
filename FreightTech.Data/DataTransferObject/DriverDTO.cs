using FreightTech.Enum;
using System;
using System.Collections.Generic;

namespace FreightTech.Data.DataTransferObject
{
    public class DriverDTO : UserProfileDTO
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string Contact { get; set; }
        public string VehicleNumber { get; set; }
        public int StatusId { get; set; }
        public DriverStatus Status { get; set; }
        public bool IsLoaded { get; set; }
        public bool IsAccepted { get; set; }
        public string CurrentLatitude { get; set; }
        public string CurrentLongitude { get; set; }
        public DateTime? LastLocationUpdated { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }

    public class DriverLocationDTO
    {
        public int DriverId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class abc
    {

    }
}

