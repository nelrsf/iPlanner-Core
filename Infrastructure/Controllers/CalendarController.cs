using iPlanner.Application.DTO.Calendar;
using iPlanner.Application.Services.Calendar;
using Microsoft.AspNetCore.Mvc;

namespace iPlanner.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;
        private readonly CalendarDateCalculator _calendarDateCalculator;

        /// <summary>
        /// Constructor de la clase CalendarController.
        /// </summary>
        /// <param name="calendarService">Servicio de calendario inyectado.</param>
        /// <param name="calendarDateCalculator">Calculadora de fechas de calendario inyectada.</param>
        public CalendarController(ICalendarService calendarService, CalendarDateCalculator calendarDateCalculator)
        {
            _calendarService = calendarService;
            _calendarDateCalculator = calendarDateCalculator;
        }

        /// <summary>
        /// Obtiene un día del calendario por su fecha.
        /// </summary>
        /// <param name="date">La fecha para la cual se desea obtener la información del calendario.</param>
        /// <returns>La información del día del calendario para la fecha especificada.</returns>
        /// <response code="200">Si la operación se realiza con éxito.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpGet("calendar-day")]
        [EndpointDescription("Obtener información detallada de un día del calendario por una fecha específica, incluyendo si es feriado, fin de semana o día no laborable.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CalendarDayDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCalendarDay(DateTime date)
        {
            try
            {
                var calendarDay = await _calendarService.GetCalendarDay(date);
                return Ok(calendarDay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene los días del calendario para una semana específica en un año dado.
        /// </summary>
        /// <param name="week">El número de la semana dentro del año.</param>
        /// <param name="year">El año para el cual se busca la información.</param>
        /// <returns>Una lista de los días del calendario para la semana y año especificados.</returns>
        /// <response code="200">Si la operación se realiza con éxito.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpGet("calendar-days")]
        [EndpointDescription("Obtener una lista de días del calendario para una semana específica en un año dado, incluyendo información sobre si son feriados, fines de semana o días no laborables.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CalendarDayDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCalendarDays(int week, int year)
        {
            try
            {
                var calendarDays = await _calendarService.GetCalendarDays(week, year);
                return Ok(calendarDays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene la fecha del primer día de una semana específica.
        /// </summary>
        /// <param name="week">El número de la semana dentro del año.</param>
        /// <param name="year">El año para el cual se busca la información.</param>
        /// <returns>La fecha correspondiente al primer día de la semana especificada.</returns>
        /// <response code="200">Si la operación se realiza con éxito.</response>
        /// <response code="404">Si no se encuentran días para la semana especificada.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpGet("first-day-of-week")]
        [EndpointDescription("Obtener la fecha del primer día de una semana específica en un año dado.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DateTime))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFirstDateOfWeek(int week, int year)
        {
            try
            {
                var firstDay = _calendarDateCalculator.GetFirstDateOfWeek(year, week);
                return Ok(firstDay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene la fecha del último día de una semana específica.
        /// </summary>
        /// <param name="week">El número de la semana dentro del año.</param>
        /// <param name="year">El año para el cual se busca la información.</param>
        /// <returns>La fecha correspondiente al último día de la semana especificada.</returns>
        /// <response code="200">Si la operación se realiza con éxito.</response>
        /// <response code="404">Si no se encuentran días para la semana especificada.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpGet("last-day-of-week")]
        [EndpointDescription("Obtener la fecha del último día de una semana específica en un año dado.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DateTime))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetLastDateOfWeek(int week, int year)
        {
            try
            {
                var lastDay = _calendarDateCalculator.GetLastDateOfWeek(year, week);
                return Ok(lastDay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Marca un día específico como feriado.
        /// </summary>
        /// <param name="calendarDay">El día a marcar como feriado.</param>
        /// <returns>Resultado de la operación.</returns>
        /// <response code="200">Si la operación se realiza con éxito.</response>
        /// <response code="400">Si los datos proporcionados no son válidos.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpPost("set-holiday")]
        [EndpointDescription("Marcar un día específico como feriado.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetHoliday([FromBody] CalendarDayDTO calendarDay)
        {
            if (calendarDay == null)
            {
                return BadRequest(new { message = "Los datos del día no pueden ser nulos." });
            }

            try
            {
                await _calendarService.SetHoliday(calendarDay);
                return Ok(new { message = "El día ha sido marcado como feriado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Desmarca un día específico como feriado.
        /// </summary>
        /// <param name="calendarDay">El día a desmarcar como feriado.</param>
        /// <returns>Resultado de la operación.</returns>
        /// <response code="200">Si la operación se realiza con éxito.</response>
        /// <response code="400">Si los datos proporcionados no son válidos.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpPost("unset-holiday")]
        [EndpointDescription("Desmarcar un día específico como feriado.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UnsetHoliday([FromBody] CalendarDayDTO calendarDay)
        {
            if (calendarDay == null)
            {
                return BadRequest(new { message = "Los datos del día no pueden ser nulos." });
            }

            try
            {
                await _calendarService.UnsetHoliday(calendarDay);
                return Ok(new { message = "El día ha sido desmarcado como feriado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
