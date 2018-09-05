using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FreightTech.Data.User
{
    public class UserProfile: RowObject
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        //public string UserName { get; set; }        
        public int RoleId { get; set; }
        public bool HasImage { get; set; }

        //public virtual UserRole UserRoles { get; set; }
    }

    public class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            HasKey(a => a.UserId);

            Property(t => t.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FirstName).IsRequired();
            Property(t => t.LastName).IsRequired();
            Property(t => t.EmailId).IsRequired();
            //Property(t => t.UserName).IsRequired();
            Property(t => t.RoleId).IsRequired();

            ToTable("UserProfile");
            //HasRequired(t => t.UserRoles).WithMany(c => c.UserProfile).HasForeignKey
            //       (t => t.UserRoleId).WillCascadeOnDelete(false);
        }
    }
}
