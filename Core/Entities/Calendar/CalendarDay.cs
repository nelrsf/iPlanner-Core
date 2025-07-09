namespace iPlanner.Entities.Calendar
{
    public class CalendarDay
    {
        public DateTime Date { get; set; }
        public bool IsToday { get; set; }
        public bool IsWeekend { get; set; }
        public bool IsHoliday { get; set; }

        public bool NonWorkingDay
        {
            get
            {
                return IsWeekend || IsHoliday;
            }
        }

        public CalendarDay(DateTime date)
        {
            Date = date;
            IsToday = date.Date == DateTime.Today;
            IsWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
            IsHoliday = false;
        }
    }
}
