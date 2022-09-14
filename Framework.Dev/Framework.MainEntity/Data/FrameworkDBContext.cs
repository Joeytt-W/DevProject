using Framework.MainEntity.DbEntity;
using System;
using System.Data.Entity;

namespace Framework.MainEntity.Data
{
    public class FrameworkDBContext:DbContext
    {
        public FrameworkDBContext() :base("name=MainConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Company>()
                .Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>()
                .Property(x => x.Country).HasMaxLength(50);
            modelBuilder.Entity<Company>()
                .Property(x => x.Industry).HasMaxLength(50);
            modelBuilder.Entity<Company>()
                .Property(x => x.Product).HasMaxLength(100);
            modelBuilder.Entity<Company>()
                .Property(x => x.Introduction).HasMaxLength(500);

            modelBuilder.Entity<Employee>()
                .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(50);

            //modelBuilder.Entity<Employee>()
            //    .HasOne(x => x.Company)
            //    .WithMany(x => x.Employees)
            //    .HasForeignKey(x => x.CompanyId)
            //    .OnDelete(DeleteBehavior.Cascade);
            
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DevLog> DevLogs { get; set; }
    }
}
