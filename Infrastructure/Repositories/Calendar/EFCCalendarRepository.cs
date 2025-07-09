using iPlanner.Application.DTO.Calendar;
using iPlanner.Application.Services.Calendar;
using iPlanner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace iPlanner.Infrastructure.Repositories.Calendar
{
    public class EFCCalendarRepository : ICalendarRepository
    {
        private ApplicationDbContext _context;
        public EFCCalendarRepository(ApplicationDbContext applicationDbContext) {
            _context = applicationDbContext;
        }

        public Task<CalendarDayDTO> GetCalendarDay(DateTime date)
        {
            var fcn = new Func<CalendarDayDTO, bool>((CalendarDayDTO d) =>
            {
                // Comparar fechas sin la hora
                return d.Date.Date == date.Date;
            });
            // Traer de la bd solo el registro que coindida con la fecha
            var calendarDay = _context.CalendarDays.AsNoTracking().FirstOrDefault(fcn);

            if (calendarDay == null)
            {
                return Task.FromResult<CalendarDayDTO>(null);
            }
            calendarDay.IsToday = calendarDay.Date.Date == DateTime.Now.Date;
            return Task.FromResult(calendarDay);
        }

        public Task<bool> UpsertCalendarDay(CalendarDayDTO calendarDay)
        {
            var existingCalendarDay = _context.CalendarDays.FirstOrDefault(d => d.Date.Date == calendarDay.Date.Date);
            if (existingCalendarDay != null)
            {
                existingCalendarDay.IsToday = calendarDay.IsToday;
                existingCalendarDay.IsWeekend = calendarDay.IsWeekend;
                existingCalendarDay.IsHoliday = calendarDay.IsHoliday;
                existingCalendarDay.NonWorkingDay = calendarDay.NonWorkingDay;
            }
            else
            {
                _context.CalendarDays.Add(calendarDay);
            }
            return Task.FromResult(_context.SaveChanges() > 0);
        }
    }
}
