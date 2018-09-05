using FreightTech.Data.User;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace FreightTech.Data
{
    public class FreightTechContext : DbContext
    {
        public FreightTechContext()
            : base("name=FreightTechConnection")
        {
        }

        private static FreightTechContext instance;

        public static FreightTechContext Instance {
            get {
                if (instance == null)
                {
                    instance = new FreightTechContext();
                }
                return instance;
            }
        }

        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<DriverDetail> DriverDetail { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ApplicationLog> ApplicationLog { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<SnapUpContext>(new CreateDatabaseIfNotExists<SnapUpContext>());
            //modelBuilder.Entity<User>()
            //    .Property(u => u.DisplayName)
            //    .HasColumnName("display_name");
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
