global using DemoDataAccess.Models;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DemoDataAccess.Models.DepartmentModel;


namespace DemoDataAccess.Data.Configurations
{
    public class DepartmentConfigurations :BaseEntityConfiguration<Department> , IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.Property(D => D.Id).UseIdentityColumn(10,10);
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D => D.Code).HasColumnType("varchar(20)");
            builder.HasMany(D => D.Employees)
                .WithOne(E => E.Department)
                .HasForeignKey(E => E.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            base.Configure(builder);

        }
    }
}
