using iPlanner.Entities.Reports;

namespace iPlanner.Entities.Teams.ConflictSchedule
{
    public interface IConflictScheduleRule
    {
        IEnumerable<ConflictItem> EvaluateConflicts(Report report, List<Report> reports);
    }

}
