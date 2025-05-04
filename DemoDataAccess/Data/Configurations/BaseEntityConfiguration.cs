using DemoDataAccess.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess.Data.Configurations
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity

    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
