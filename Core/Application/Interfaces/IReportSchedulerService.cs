using iPlanner.Application.DTO;

namespace iPlanner.Application.Interfaces
{
    public interface IReportSchedulerService
    {
        Task<bool> HasOvertime(ReportsDTO report);

        double GetReportHours(ReportsDTO report);

        int GetReportWeekNumber(ReportsDTO report);
    }
}