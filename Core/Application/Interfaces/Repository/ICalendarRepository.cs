using iPlanner.Application.DTO.Calendar;

namespace iPlanner.Application.Services.Calendar
{
    public interface ICalendarRepository
    {
        /// <summary>
        /// Get a calendar day by specific date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>A calendar day entity</returns>
        /// 
        Task<CalendarDayDTO> GetCalendarDay(DateTime date);

        /// <summary>
        /// Update a calendar day, if does not exist, create a new one
        /// </summary>
        /// <param name="calendarDay"></param>
        /// <returns>True if creation or update was successful, false in other case</returns>
        /// 
        Task<bool> UpsertCalendarDay(CalendarDayDTO calendarDay);
    }
}