using iPlanner.Core.Application.DTO.Calendar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iPlanner_Core.Infrastructure.Repositories.EFC.Maps
{
    public class CalendarDayMap : IEntityTypeConfiguration<CalendarDayDTO>
    {
        public void Configure(EntityTypeBuilder<CalendarDayDTO> builder)
        {
            builder.ToTable("dias_calendario");

            builder.HasKey(cd => cd.Date);

            builder.Property(cd => cd.Date)
                .HasColumnName("fecha")
                .HasConversion(new UtcDateTimeConverter())
                .IsRequired();
            builder.Ignore(cd => cd.IsToday);
            builder.Property(cd => cd.IsWeekend)
                .HasColumnName("fds")
                .IsRequired();
            builder.Property(cd => cd.IsHoliday)
                .HasColumnName("festivo")
                .IsRequired();
            builder.Property(cd => cd.NonWorkingDay)
                .HasColumnName("no_laboral")
                .IsRequired();
        }
    }
}
