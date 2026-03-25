using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeANumber.Models;

namespace TakeANumber.Data.Mappings
{
    public class SpotMap : IEntityTypeConfiguration<Spot>
    {
        public void Configure(EntityTypeBuilder<Spot> builder)
        {
            builder.ToTable("Spot");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Enabled)
                .HasColumnName("Enabled")
                .HasDefaultValue(true);
        }
    }
}
