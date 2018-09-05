using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FreightTech.Data
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int? DriverId { get; set; }
        public int StatusId { get; set; }
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

    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {

            HasKey(a => a.OrderId);
            Property(t => t.OrderId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CustomerId).IsRequired();

            Property(t => t.DriverId);
            Property(t => t.StatusId).IsRequired();
            Property(t => t.PickupLocation).IsRequired();
            Property(t => t.DropoffLocation).IsRequired();
            Property(t => t.PickupLatitude).IsRequired();

            Property(t => t.PickupLongitude).IsRequired();
            Property(t => t.DropoffLatitude).IsRequired();
            Property(t => t.DropoffLongitude).IsRequired();
            Property(t => t.LoadType).IsRequired();

            Property(t => t.Commodity).IsRequired();
            Property(t => t.Weight).IsRequired();
            Property(t => t.ContactNumber).IsRequired();

            Property(t => t.DateCreated);

            ToTable("Order");
        }
    }
}
