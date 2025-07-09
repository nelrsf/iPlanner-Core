using iPlanner.Entities.Reports;

namespace iPlanner.Entities.Teams.ConflictSchedule
{
    public class OverlappingTimeLimitsRule : IConflictScheduleRule
    {
        public IEnumerable<ConflictItem> EvaluateConflicts(Report report, List<Report> reports)
        {
            var conflicts = new List<ConflictItem>();
            var dayOfWeek = report.Date?.DayOfWeek;

            if (dayOfWeek == null) return conflicts;

            var scheduleStart = ReportScheduleUpdater.GetTimeInit(dayOfWeek.Value);
            var scheduleEnd = ReportScheduleUpdater.GetTimeEnd(dayOfWeek.Value);

            if (report.TimeInit < scheduleStart || report.TimeEnd > scheduleEnd)
            {
                conflicts.Add(new ConflictItem
                {
                    Team = report.Team,
                    Date = report.Date.Value,
                    Description = "El tiempo del reporte está por fuera del horario regular."
                });
            }

            return conflicts;
        }
    }
}
