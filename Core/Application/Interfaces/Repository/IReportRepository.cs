using iPlanner.Application.DTO;

namespace iPlanner.Application.Interfaces.Repository
{
    public interface IReportRepository
    {
        public Task<List<ReportsDTO>> GetReports();
        public Task AddReport(ReportsDTO report);
        public Task UpdateReport(ReportsDTO updatedReport);
        public Task DeleteReport(ReportsDTO report);

    }
}
