using DemoDataAccess.Models.DepartmentModel;
using DemoDataAccess.Models.IdentityModel;
using DemoDataAccess.Models.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace DemoDataAccess.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Connection string");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BaseEntity>();
            //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            /* modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);*/ //في حالة لو الassembly موجود ف مشروع تاني  
            base.OnModelCreating(modelBuilder);
        }

    }
}
