using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeANumber.Enums;
using TakeANumber.Models;

namespace TakeANumber.Data.Mappings
{
    public class TicketNumberMap : IEntityTypeConfiguration<TicketNumber>
    {
        public void Configure(EntityTypeBuilder<TicketNumber> builder)
        {
            builder.ToTable("TicketNumber");

            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();
            builder.Property(x => x.Ticket)
                .IsRequired()
                .HasColumnName("TicketNumber");
            builder.Property(x=>x.TicketType)
                .HasColumnName("TicketType")
                .HasConversion<TicketType>()
                .HasDefaultValue(TicketType.Regular);
            builder.Property(x => x.Called)
                .HasDefaultValue(false)
                .HasColumnName("Called");
            builder.Property(x=>x.Serviced)
                .HasDefaultValue(false)
                .HasColumnName("Serviced");
            builder.Property(x => x.GenerateDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .HasColumnName("GenerateDate");
            builder.Property(x => x.ServicedDate)
                .HasColumnName("ServicedDate");
            builder.Property(x => x.CalledDate)
                .HasColumnName("CalledDate");


            //Relacionamentos
            builder.HasOne(x => x.Spot)
                .WithMany()
                .HasForeignKey("SpotId")
                .HasConstraintName("FK_SpotTicketNumber_SpotId")
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey("ComapnyId")
                .HasConstraintName("FK_CompanyTicketNumber_CompanyId")
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(x => x.TicketGroup)
                .WithMany()
                .HasForeignKey("TicketGroupId")
                .HasConstraintName("FK_TicketGroupTicketNumber_TicketGroupId")
                .OnDelete(DeleteBehavior.ClientNoAction); ;
        }
    }
}
