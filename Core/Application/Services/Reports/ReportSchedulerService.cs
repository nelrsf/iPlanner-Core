using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Calendar;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Services.Calendar;
using iPlanner.Core.Entities.Reports;

namespace iPlanner.Core.Application.Services.Reports
{
    public class ReportSchedulerService : IReportSchedulerService
    {
        private readonly IMapper<ReportsDTO, Report> _reportMapper;
        private readonly ICalendarService _calendarService;

        public ReportSchedulerService(IMapper<ReportsDTO, Report> reportMapper, ICalendarService calendar)
        {
            _reportMapper = reportMapper;
            _calendarService = calendar;
        }



        /// <summary>
        /// Verifica si un reporte tiene horas extras.
        /// </summary>
        /// <param name="report">El objeto ReportsDTO que representa el reporte a evaluar.</param>
        /// <returns>
        /// Devuelve true si el reporte tiene horas extras, ya sea porque el día es no laborable
        /// o porque el reporte en sí indica que tiene horas extras. Devuelve false en caso contrario.
        /// </returns>
        public async Task<bool> HasOvertime(ReportsDTO report)
        {
            // Convierte el objeto ReportsDTO en una entidad Report utilizando el mapeador.
            Report reportEntity = _reportMapper.ToEntity(report);

            // Verifica si la fecha del reporte no tiene valor. Si no tiene, no puede haber horas extras.
            if (!reportEntity.Date.HasValue)
            {
                return false;
            }

            // Obtiene información del día del calendario correspondiente a la fecha del reporte.
            CalendarDayDTO calendarDay = await _calendarService.GetCalendarDay(reportEntity.Date.Value);

            // Si no se encuentra información del día en el calendario, no puede haber horas extras.
            if (calendarDay == null)
            {
                return false;
            }

            // Si el día es no laborable (por ejemplo, un fin de semana o feriado), se considera que hay horas extras.
            if (calendarDay.NonWorkingDay)
            {
                return true;
            }

            // Finalmente, verifica si el reporte en sí indica que tiene horas extras.
            return reportEntity.HasOvertime();
        }

        public int GetReportWeekNumber(ReportsDTO report)
        {
            Report reportEntity = _reportMapper.ToEntity(report);
            return reportEntity.WeekNumber;
        }

        public double GetReportHours(ReportsDTO report)
        {
            Report reportEntity = _reportMapper.ToEntity(report);
            return reportEntity.TotalHours;
        }
    }
}
