namespace iPlanner.Core.Application.DTO.Calendar
{
    public class CalendarDayDTO
    {
        public DateTime Date { get; set; }
        public bool IsToday { get; set; }
        public bool IsWeekend { get; set; }
        public bool IsHoliday { get; set; }
        public bool NonWorkingDay { get; set; }
    }
}
