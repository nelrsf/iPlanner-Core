using iPlanner.Entities.Reports;
using iPlanner.Entities.Teams;

namespace iPlanner.Application.Services
{
    public interface IConflictValidationService
    {
        void ComputeConflicts(ScheduleTeamData scheduleTeamData, List<Report> reports);
    }
}