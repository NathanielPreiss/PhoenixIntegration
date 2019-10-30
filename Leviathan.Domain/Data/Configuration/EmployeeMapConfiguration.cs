using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leviathan
{
    public class EmployeeMapConfiguration : IEntityTypeConfiguration<EmployeeMap>
    {
        public void Configure(EntityTypeBuilder<EmployeeMap> builder)
        {
            builder.HasKey(x => new { x.PhoenixId, x.LeviathanId });

            builder.Property(x => x.PhoenixId);
            builder.Property(x => x.LeviathanId);
        }
    }
}
