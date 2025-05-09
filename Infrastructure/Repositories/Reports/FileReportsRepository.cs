using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Interfaces.Repository;
using iPlanner.Infrastructure.Common;

namespace iPlanner_Core.Infrastructure.Repositories.Reports
{
    public class FileReportsRepository : IReportRepository
    {
        private readonly IFileService _fileService;
        private readonly string _reportsFilePath;

        public FileReportsRepository(IFileService fileService)
        {
            _fileService = fileService;
            _reportsFilePath = _fileService.GetDataFilePath("Reports.json");
            _fileService.EnsureDirectoryExists(_reportsFilePath);
        }

        public async Task<List<ReportsDTO>> GetReports()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var reports = _fileService.LoadJsonData<List<ReportsDTO>>(_reportsFilePath) ?? new List<ReportsDTO>();
                    return reports.OrderByDescending(r => r.Date).ToList();
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading reports", ex);
            }
        }

        public async Task AddReport(ReportsDTO report)
        {
            var reports = await GetReports();
            report.ReportId = IdGenerator.GenerateUUID();
            reports.Add(report);
            SaveReports(reports);
        }

        public async Task UpdateReport(ReportsDTO updatedReport)
        {
            var reports = await GetReports();
            var index = reports.FindIndex(r => r.ReportId == updatedReport.ReportId);
            if (index != -1)
            {
                reports[index] = updatedReport;
                SaveReports(reports);
            }
        }

        public async Task DeleteReport(ReportsDTO report)
        {
            var reports = await GetReports();
            reports.RemoveAll(r => r.ReportId == report.ReportId);
            SaveReports(reports);
        }

        private void SaveReports(List<ReportsDTO> reports)
        {
            if (reports == null)
                throw new ArgumentNullException(nameof(reports));

            try
            {
                _fileService.SaveJsonData(_reportsFilePath, reports);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving reports", ex);
            }
        }
    }
}
