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
        /// Obtiene un d�a del calendario por su fecha.
        /// </summary>
        /// <param name="date">La fecha para la cual se desea obtener la informaci�n del calendario.</param>
        /// <returns>La informaci�n del d�a del calendario para la fecha especificada.</returns>
        /// <response code="200">Si la operaci�n se realiza con �xito.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpGet("calendar-day")]
        [EndpointDescription("Obtener informaci�n detallada de un d�a del calendario por una fecha espec�fica, incluyendo si es feriado, fin de semana o d�a no laborable.")]
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
        /// Obtiene los d�as del calendario para una semana espec�fica en un a�o dado.
        /// </summary>
        /// <param name="week">El n�mero de la semana dentro del a�o.</param>
        /// <param name="year">El a�o para el cual se busca la informaci�n.</param>
        /// <returns>Una lista de los d�as del calendario para la semana y a�o especificados.</returns>
        /// <response code="200">Si la operaci�n se realiza con �xito.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpGet("calendar-days")]
        [EndpointDescription("Obtener una lista de d�as del calendario para una semana espec�fica en un a�o dado, incluyendo informaci�n sobre si son feriados, fines de semana o d�as no laborables.")]
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
        /// Obtiene la fecha del primer d�a de una semana espec�fica.
        /// </summary>
        /// <param name="week">El n�mero de la semana dentro del a�o.</param>
        /// <param name="year">El a�o para el cual se busca la informaci�n.</param>
        /// <returns>La fecha correspondiente al primer d�a de la semana especificada.</returns>
        /// <response code="200">Si la operaci�n se realiza con �xito.</response>
        /// <response code="404">Si no se encuentran d�as para la semana especificada.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpGet("first-day-of-week")]
        [EndpointDescription("Obtener la fecha del primer d�a de una semana espec�fica en un a�o dado.")]
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
        /// Obtiene la fecha del �ltimo d�a de una semana espec�fica.
        /// </summary>
        /// <param name="week">El n�mero de la semana dentro del a�o.</param>
        /// <param name="year">El a�o para el cual se busca la informaci�n.</param>
        /// <returns>La fecha correspondiente al �ltimo d�a de la semana especificada.</returns>
        /// <response code="200">Si la operaci�n se realiza con �xito.</response>
        /// <response code="404">Si no se encuentran d�as para la semana especificada.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpGet("last-day-of-week")]
        [EndpointDescription("Obtener la fecha del �ltimo d�a de una semana espec�fica en un a�o dado.")]
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
        /// Marca un d�a espec�fico como feriado.
        /// </summary>
        /// <param name="calendarDay">El d�a a marcar como feriado.</param>
        /// <returns>Resultado de la operaci�n.</returns>
        /// <response code="200">Si la operaci�n se realiza con �xito.</response>
        /// <response code="400">Si los datos proporcionados no son v�lidos.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpPost("set-holiday")]
        [EndpointDescription("Marcar un d�a espec�fico como feriado.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetHoliday([FromBody] CalendarDayDTO calendarDay)
        {
            if (calendarDay == null)
            {
                return BadRequest(new { message = "Los datos del d�a no pueden ser nulos." });
            }

            try
            {
                await _calendarService.SetHoliday(calendarDay);
                return Ok(new { message = "El d�a ha sido marcado como feriado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Desmarca un d�a espec�fico como feriado.
        /// </summary>
        /// <param name="calendarDay">El d�a a desmarcar como feriado.</param>
        /// <returns>Resultado de la operaci�n.</returns>
        /// <response code="200">Si la operaci�n se realiza con �xito.</response>
        /// <response code="400">Si los datos proporcionados no son v�lidos.</response>
        /// <response code="500">Si ocurre un error inesperado en el servidor.</response>
        [HttpPost("unset-holiday")]
        [EndpointDescription("Desmarcar un d�a espec�fico como feriado.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UnsetHoliday([FromBody] CalendarDayDTO calendarDay)
        {
            if (calendarDay == null)
            {
                return BadRequest(new { message = "Los datos del d�a no pueden ser nulos." });
            }

            try
            {
                await _calendarService.UnsetHoliday(calendarDay);
                return Ok(new { message = "El d�a ha sido desmarcado como feriado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
