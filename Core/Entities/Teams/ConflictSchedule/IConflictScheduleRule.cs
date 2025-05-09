using iPlanner.Core.Entities.Reports;

namespace iPlanner.Core.Entities.Teams.ConflictSchedule
{
    public interface IConflictScheduleRule
    {
        IEnumerable<ConflictItem> EvaluateConflicts(Report report, List<Report> reports);
    }

}
