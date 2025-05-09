using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Services.Reports;
using Microsoft.AspNetCore.Mvc;

namespace iPlanner.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportSchedulerController : ControllerBase
    {
        private readonly ReportSchedulerService _reportSchedulerService;

        public ReportSchedulerController(ReportSchedulerService reportSchedulerService)
        {
            _reportSchedulerService = reportSchedulerService;
        }

        /// <summary>
        /// Verifica si un reporte tiene horas extras.
        /// </summary>
        /// <param name="report">El reporte a evaluar.</param>
        /// <returns>True si tiene horas extras, false en caso contrario.</returns>
        [HttpPost("has-overtime")]
        public async Task<IActionResult> HasOvertime([FromBody] ReportsDTO report)
        {
            var result = await _reportSchedulerService.HasOvertime(report);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene el número de semana de un reporte.
        /// </summary>
        /// <param name="report">El reporte.</param>
        /// <returns>El número de semana.</returns>
        [HttpPost("week-number")]
        public IActionResult GetReportWeekNumber([FromBody] ReportsDTO report)
        {
            var result = _reportSchedulerService.GetReportWeekNumber(report);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene las horas totales de un reporte.
        /// </summary>
        /// <param name="report">El reporte.</param>
        /// <returns>Las horas totales.</returns>
        [HttpPost("total-hours")]
        public IActionResult GetReportHours([FromBody] ReportsDTO report)
        {
            var result = _reportSchedulerService.GetReportHours(report);
            return Ok(result);
        }
    }
}
