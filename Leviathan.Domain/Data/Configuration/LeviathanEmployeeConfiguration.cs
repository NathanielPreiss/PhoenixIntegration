using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leviathan
{
    public class LeviathanEmployeeConfiguration : IEntityTypeConfiguration<LeviathanEmployee>
    {
        public void Configure(EntityTypeBuilder<LeviathanEmployee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.FirstName);
            builder.Property(x => x.LastName);
            builder.Property(x => x.Role);
            builder.Property(x => x.Email);
            builder.Property(x => x.Telephone);
        }
    }
}