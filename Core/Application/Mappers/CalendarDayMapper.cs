using iPlanner.Core.Application.DTO.Calendar;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Calendar;

namespace iPlanner.Core.Application.Mappers
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
