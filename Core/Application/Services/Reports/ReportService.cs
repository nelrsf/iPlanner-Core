using iPlanner.Application.DTO;
using iPlanner.Application.DTO.Reports;
using iPlanner.Application.Interfaces;
using iPlanner.Application.Interfaces.Repository;
using iPlanner.Entities.Reports;

namespace iPlanner.Application.Services.Reports
{
    public class ReportsService : IReportService
    {
        private readonly IReportRepository _reportsRepository;
        private readonly IMapper<ReportsDTO, Report> _reportMapper;
        private readonly IMapper<ReportFilterDTO, ReportFilter> _filterMapper;

        public ReportsService(
                IReportRepository repository,
                IMapper<ReportsDTO, Report> reportMapper,
                IMapper<ReportFilterDTO, ReportFilter> reportFilterMapper)
        {
            _reportsRepository = repository;
            _reportMapper = reportMapper;
            _filterMapper = reportFilterMapper;
        }

        public async Task<List<ReportsDTO>> GetReportsAsync()
        {
            return await _reportsRepository.GetReports();
        }

        public async Task AddReportsAsync(ReportsDTO report)
        {
            await _reportsRepository.AddReport(report);
        }

        public async Task DeleteReportsAsync(ReportsDTO report)
        {
            await _reportsRepository.DeleteReport(report);
        }

        public async Task UpdateReportsAsync(ReportsDTO updatedReport)
        {
            await _reportsRepository.UpdateReport(updatedReport);
        }

        public ReportsDTO InitializeNewReports()
        {
            Report report = new Report();
            return _reportMapper.ToDTO(report);
        }

        public ReportsDTO RefreshReportsDate(ReportsDTO dto)
        {
            Report report = _reportMapper.ToEntity(dto);
            report.Date = dto.Date;
            dto = _reportMapper.ToDTO(report);
            return dto;
        }

        public async Task<List<ReportsDTO>> GetReportsAsyncByFilter(ReportFilterDTO filterDTO)
        {
            ReportFilter filter = _filterMapper.ToEntity(filterDTO);
            if (filter.CheckTime())
            {
                List<ReportsDTO> reportsDTO = await GetReportsAsync();
                List<Report> reports = reportsDTO.Select(r => _reportMapper.ToEntity(r)).ToList();
                List<Report> filteredReports = filter.FilterReports(reports);
                return filteredReports.Select(fr => _reportMapper.ToDTO(fr)).ToList();
            }
            else
            {
                throw new Exception("Error, la fecha de inicio debe ser menor a la fecha fin");
            }

        }

        public ReportsDTO ChangeReportTeam(ReportsDTO dto)
        {
            Report report = _reportMapper.ToEntity(dto);
            report.UpdateTeamMembers();
            return _reportMapper.ToDTO(report);
        }

        public async Task DuplicateReport(ReportsDTO dto)
        {
            Report report = _reportMapper.ToEntity(dto);
            var clonedReport = report.Clone();
            var clonedReportDTO = _reportMapper.ToDTO(clonedReport);
            await _reportsRepository.AddReport(clonedReportDTO);
        }
    }
}
