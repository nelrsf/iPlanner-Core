using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Calendar;
using iPlanner_Core.Infrastructure.Repositories.EFC.Maps;
using Microsoft.EntityFrameworkCore;

namespace iPlanner.Core.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<LocationItemsDTO> Locations { get; set; }
        public DbSet<CalendarDayDTO> CalendarDays { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CalendarDayMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
