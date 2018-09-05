using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FreightTech.Data
{
    public class Feedback
    {
      
        public int FbId { get; set; }
        public string Feedbacks { get; set; }
        public int UserId { get; set; }
       
       
       
    }

    public class FeedbackConfiguration : EntityTypeConfiguration<Feedback>
    {
        public FeedbackConfiguration()
        {

            HasKey(a => a.FbId);
            Property(t => t.FbId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.UserId).IsRequired();

            Property(t => t.Feedbacks).IsRequired();
           


            ToTable("Feedback");
        }
    }
}
