using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Reports;

namespace iPlanner.Core.Application.Interfaces
{
    public interface IReportService
    {

        Task<List<ReportsDTO>> GetReportsAsync();

        Task<List<ReportsDTO>> GetReportsAsyncByFilter(ReportFilterDTO filterDTO);

        Task AddReportsAsync(ReportsDTO report);


        Task UpdateReportsAsync(ReportsDTO report);


        Task DeleteReportsAsync(ReportsDTO report);

        ReportsDTO InitializeNewReports();
        ReportsDTO RefreshReportsDate(ReportsDTO dto);

        ReportsDTO ChangeReportTeam(ReportsDTO dto);

        Task DuplicateReport(ReportsDTO dto);
    }
}
