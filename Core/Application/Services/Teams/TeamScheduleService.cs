using iPlanner.Application.DTO;
using iPlanner.Application.DTO.Teams;
using iPlanner.Application.Interfaces;
using iPlanner.Application.Interfaces.Repository;
using iPlanner.Entities.Reports;
using iPlanner.Entities.Teams;

namespace iPlanner.Application.Services.Teams
{
    public class TeamScheduleService : ITeamScheduleService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IConflictValidationService _conflictValidationService;
        private readonly IMapper<ScheduleTeamDataDTO, ScheduleTeamData> _scheduleTeamDataMapper;
        private readonly IMapper<ReportsDTO, Report> _reportMapper;

        public TeamScheduleService(IReportRepository reportRepository, 
            IConflictValidationService conflictValidationService,
            IMapper<ScheduleTeamDataDTO, ScheduleTeamData> scheduleTeamDataMapper,
            IMapper<ReportsDTO, Report> reportMapper)
        {
            _reportRepository = reportRepository;
            _conflictValidationService = conflictValidationService;
            _scheduleTeamDataMapper = scheduleTeamDataMapper;
            _reportMapper = reportMapper;
        }

        public async Task<IEnumerable<int>> GetAvailableWeeks(int year)
        {
            var reports = await _reportRepository.GetReports();
            var availableWeeks = reports
                .Where(report => report.Date.HasValue && report.Date.Value.Year == year)
                .Select(report => System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                    report.Date.Value,
                    System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule,
                    System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek))
                .Distinct()
                .OrderBy(week => week);
            return availableWeeks;
        }

        public async Task<IEnumerable<int>> GetAvailableYears()
        {
            var reports = await _reportRepository.GetReports();
            var availableYears = reports
                .Where(report => report.Date.HasValue)
                .Select(report => report.Date.Value.Year)
                .Distinct()
                .OrderBy(year => year);
            return availableYears;
        }

        public async Task<ScheduleTeamDataDTO> GetScheduleData(int year, int week)
        {
            var reportsDTO = await _reportRepository.GetReports();
            var reportEntities = reportsDTO.Select(dto => _reportMapper.ToEntity(dto)).ToList();

            var scheduleData = new ScheduleTeamData
            {
                ConflictItems = new List<ConflictItem>(),
                TeamItems = new List<ScheduleTeamItem>()
            };

            foreach (var report in reportEntities.Where(r => r.Date.HasValue))
            {
                var reportDate = report.Date.Value;
                int reportWeek = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                    reportDate,
                    System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule,
                    System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
                int reportYear = reportDate.Year;

                if (reportWeek == week && reportYear == year && report.Team != null)
                {
                    var teamItem = scheduleData.GetTeamScheduleItem(report.Team);
                    teamItem.Name = report.Team.Name ?? string.Empty;
                    teamItem.TotalHours += report.TotalHours;
                    teamItem.HasConflicts = false;
                }
            }

            _conflictValidationService.ComputeConflicts(scheduleData, reportEntities);

            return _scheduleTeamDataMapper.ToDTO(scheduleData);
        }
    }
}
