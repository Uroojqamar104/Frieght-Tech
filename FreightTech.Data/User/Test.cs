using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Spatial;

namespace FreightTech.Data
{
    public class Test
    {
        [Key]
        public int TestId { get; set; }
        public int OrderId { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class TestConfiguration : EntityTypeConfiguration<Test>
    {
        public TestConfiguration()
        {
            HasKey(a => a.TestId);

            Property(t => t.TestId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.OrderId).IsRequired();
            Property(t => t.Lat).IsRequired();
            Property(t => t.Long).IsRequired();
            Property(t => t.DateCreated);

            ToTable("Test");
        }
    }
}

