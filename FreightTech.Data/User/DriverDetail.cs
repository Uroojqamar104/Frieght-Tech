using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FreightTech.Data
{
    public class DriverDetail
    {
        public int Id { get; set; }
        public int DriverId { get; set; }        
        public string Contact { get; set; }
        public string VehicleNumber { get; set; }
        public int StatusId { get; set; }
        public bool IsLoaded { get; set; }
        public bool IsAccepted { get; set; }
        public string CurrentLatitude { get; set; }
        public string CurrentLongitude { get; set; }
        public DateTime? LastLocationUpdated { get; set; }
    }

    public class DriverDetailConfiguration : EntityTypeConfiguration<DriverDetail>
    {
        public DriverDetailConfiguration()
        {

            HasKey(a => a.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.DriverId).IsRequired();

            Property(t => t.Contact).IsRequired();
            Property(t => t.VehicleNumber).IsRequired();
            Property(t => t.StatusId).IsRequired();
            Property(t => t.IsLoaded).IsRequired();
            Property(t => t.IsAccepted).IsRequired();

            Property(t => t.CurrentLatitude);
            Property(t => t.CurrentLongitude);
            Property(t => t.LastLocationUpdated);


            ToTable("DriverDetail");
        }
    }
}
