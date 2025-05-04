using DemoDataAccess.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess.Data.Configurations
{
    public class EmployeeConfigurations :BaseEntityConfiguration<Employee> , IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");
         

            builder.Property(E => E.Gender)
                .HasConversion(
                (gender) => gender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );
            builder.Property(E => E.EmployeeType)
                .HasConversion(
                (empType) => empType.ToString(),
                (empType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), empType)
                );
            base.Configure(builder);
        }
    }
}
