using iPlanner.Application.DTO.Calendar;
using iPlanner.Application.Interfaces;
using iPlanner.Entities.Calendar;

namespace iPlanner.Application.Mappers
{
    public class CalendarDayMapper : IMapper<CalendarDayDTO, CalendarDay>
    {
        public CalendarDayDTO ToDTO(CalendarDay entity)
        {
            return new CalendarDayDTO
            {
                Date = entity.Date,
                IsToday = entity.IsToday,
                IsWeekend = entity.IsWeekend,
                IsHoliday = entity.IsHoliday,
                NonWorkingDay = entity.NonWorkingDay
            };
        }

        public CalendarDay ToEntity(CalendarDayDTO dto)
        {
            return new CalendarDay(dto.Date);
        }
    }
}
