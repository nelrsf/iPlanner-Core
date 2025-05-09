using iPlanner.Core.Entities.Reports;
using iPlanner.Core.Entities.Teams;

namespace iPlanner.Core.Application.Services
{
    public interface IConflictValidationService
    {
        void ComputeConflicts(ScheduleTeamData scheduleTeamData, List<Report> reports);
    }
}