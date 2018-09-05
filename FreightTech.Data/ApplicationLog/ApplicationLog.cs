using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Spatial;

namespace FreightTech.Data
{
    public class ApplicationLog
    {
        [Key]
        public int LogId { get; set; }
        public string Application { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string RequestUri { get; set; }
        public string InnerException { get; set; }
    }

    public class ApplicationLogConfiguration : EntityTypeConfiguration<ApplicationLog>
    {
        public ApplicationLogConfiguration()
        {
            HasKey(a => a.LogId);

            Property(t => t.LogId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Application).IsRequired();
            Property(t => t.Type).IsRequired();
            Property(t => t.UserId).IsRequired();
            Property(t => t.Date);
            Property(t => t.Message);
            Property(t => t.StackTrace);
            Property(t => t.RequestUri);
            Property(t => t.InnerException);

            ToTable("ApplicationLog");
        }
    }



}
