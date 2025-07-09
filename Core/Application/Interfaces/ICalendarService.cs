using iPlanner.Application.DTO.Calendar;

namespace iPlanner.Application.Services.Calendar
{
    public interface ICalendarService
    {
        /// <summary>
        /// Get a CalendarDay entity by a given date
        /// </summary>
        /// <param name="date">Week number</param>
        /// <returns> A respective CalendarDay entity </returns>
        Task<CalendarDayDTO> GetCalendarDay(DateTime date);

        /// <summary>
        /// Get a list of calendar days for a given week and tear
        /// <returns></returns>
        /// 
        /// </summary>
        /// <param name="week">Week number</param>
        /// <param name="year">year</param>
        /// <returns> List of calendar days </returns>
        Task<List<CalendarDayDTO>> GetCalendarDays(int week, int year);

        /// <summary>
        /// Set calendar day as holiday
        /// </summary>
        /// <param name="calendarDay">The day to set as holiday</param>
        Task SetHoliday(CalendarDayDTO calendarDay);


        /// <summary>
        /// Unset calendar day as holyday
        /// </summary>
        /// 
        /// <param name="calendarDay">The day to unset as holiday</param>
        /// 

        Task UnsetHoliday(CalendarDayDTO calendarDay);

    }
}
