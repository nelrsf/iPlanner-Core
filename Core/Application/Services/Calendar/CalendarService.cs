using iPlanner.Application.DTO.Calendar;
using iPlanner.Application.Interfaces;
using iPlanner.Entities.Calendar;
using Microsoft.EntityFrameworkCore;


namespace iPlanner.Application.Services.Calendar
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IMapper<CalendarDayDTO, CalendarDay> _calendarDayMapper;

        public CalendarService(ICalendarRepository calendarRepository, IMapper<CalendarDayDTO, CalendarDay> calendarDayMapper)
        {
            _calendarRepository = calendarRepository;
            _calendarDayMapper = calendarDayMapper;
        }
        public async Task<List<CalendarDayDTO>> GetCalendarDays(int week, int year)
        {
            var days = new List<CalendarDayDTO>();
            var calendarCalcularor = new CalendarDateCalculator();
            // Get the first Date of the week
            DateTime firstDateOfWeek = calendarCalcularor.GetFirstDateOfWeek(year, week);

            // Loop through the week
            for (int i = 0; i < 7; i++)
            {
                var date = firstDateOfWeek.AddDays(i);
                var calendarDay = await _calendarRepository.GetCalendarDay(date);
                if (calendarDay == null)
                {
                    calendarDay = _calendarDayMapper.ToDTO(new CalendarDay(date));
                }
                days.Add(calendarDay);
            }


            return days;
        }

        public async Task SetHoliday(CalendarDayDTO calendarDay)
        {
            var calendarDayEntity = new CalendarDay(calendarDay.Date);
            calendarDayEntity.IsHoliday = true;

            try
            {
                var result = await _calendarRepository.UpsertCalendarDay(_calendarDayMapper.ToDTO(calendarDayEntity));
                if (!result)
                {
                    throw new Exception("Error al establecer día no laboral");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la fecha", ex);
            }
        }

        public async Task UnsetHoliday(CalendarDayDTO calendarDay)
        {
            // Obtener el día del calendario existente
            var existingCalendarDay = await _calendarRepository.GetCalendarDay(calendarDay.Date);
            CalendarDay calendarDayEntity;
            if (existingCalendarDay == null)
            {
                calendarDayEntity = new CalendarDay(calendarDay.Date);
            }
            else
            {
                calendarDayEntity = _calendarDayMapper.ToEntity(existingCalendarDay);
            }

            calendarDayEntity.IsHoliday = false;
            var result = await _calendarRepository.UpsertCalendarDay(_calendarDayMapper.ToDTO(calendarDayEntity));
            if (!result)
            {
                throw new Exception("Error al establecer dia laboral");
            }
        }
        public async Task<CalendarDayDTO> GetCalendarDay(DateTime date)
        {
            var calendarDay = await _calendarRepository.GetCalendarDay(date);
            if (calendarDay == null)
            {
                calendarDay = _calendarDayMapper.ToDTO(new CalendarDay(date));
            }
            return calendarDay;
        }

    }
}
