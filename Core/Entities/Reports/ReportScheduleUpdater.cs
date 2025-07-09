namespace iPlanner.Entities.Reports
{
    public static class ReportScheduleUpdater
    {
        private static readonly Dictionary<DayOfWeek, (TimeSpan Start, TimeSpan End)> Schedule = new()
        {
            { DayOfWeek.Monday, (TimeSpan.FromHours(7), TimeSpan.FromHours(16)) },
            { DayOfWeek.Tuesday, (TimeSpan.FromHours(7), TimeSpan.FromHours(16)) },
            { DayOfWeek.Wednesday, (TimeSpan.FromHours(7), TimeSpan.FromHours(17)) },
            { DayOfWeek.Thursday, (TimeSpan.FromHours(7), TimeSpan.FromHours(17)) },
            { DayOfWeek.Friday, (TimeSpan.FromHours(7), TimeSpan.FromHours(15)) }
        };

        public static TimeSpan? GetTimeInit(DayOfWeek dayOfWeek)
        {
            if (!Schedule.ContainsKey(dayOfWeek)) return null;
            return Schedule[dayOfWeek].Start;
        }

        public static TimeSpan? GetTimeEnd(DayOfWeek dayOfWeek)
        {
            if (!Schedule.ContainsKey(dayOfWeek)) return null;
            return Schedule[dayOfWeek].End;
        }

        public static double GetRequieredTotalHours()
        {
            return Schedule.Sum(day => (day.Value.End - day.Value.Start).TotalHours);
        }

        public static void UpdateScheduleByDate(Report report)
        {
            if (report.Date == null) return;

            var dayOfWeek = report.Date.Value.DayOfWeek;
            if (Schedule.ContainsKey(dayOfWeek))
            {
                if(report.TimeInit== null)
                    report.TimeInit = Schedule[dayOfWeek].Start;
                if (report.TimeEnd == null)
                    report.TimeEnd = Schedule[dayOfWeek].End;
            }
        }
    }
}
