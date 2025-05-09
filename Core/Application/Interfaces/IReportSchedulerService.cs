using iPlanner.Core.Application.DTO;

namespace iPlanner.Core.Application.Interfaces
{
    public interface IReportSchedulerService
    {
        Task<bool> HasOvertime(ReportsDTO report);

        double GetReportHours(ReportsDTO report);

        int GetReportWeekNumber(ReportsDTO report);
    }
}