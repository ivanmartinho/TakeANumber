using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeANumber.Models;

namespace TakeANumber.Data.Mappings
{
    public class TicketGroupMap:IEntityTypeConfiguration<TicketGroup>
    {
        public void Configure(EntityTypeBuilder<TicketGroup> builder)
        {
            builder.ToTable("TicketGroup");

            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Acronym)
                .IsRequired()
                .HasColumnName("Acronym")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(3);

            builder.Property(x => x.Enabled)
                .HasColumnName("Enabled")
                .HasDefaultValue(true);
        }
    }
}
