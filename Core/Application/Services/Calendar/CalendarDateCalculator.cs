using System.Globalization;

namespace iPlanner.Application.Services.Calendar
{
    /// <summary>
    /// Proporciona métodos para realizar cálculos relacionados con fechas en un calendario,
    /// como obtener el primer y último día de una semana específica.
    /// </summary>
    public class CalendarDateCalculator
    {
        /// <summary>
        /// Valida los parámetros de año y número de semana.
        /// </summary>
        /// <param name="year">El año a validar.</param>
        /// <param name="weekOfYear">El número de la semana a validar.</param>
        /// <exception cref="ArgumentException">Se lanza si los parámetros no son válidos.</exception>
        private void ValidateYearAndWeek(int year, int weekOfYear)
        {
            if (year < 1)
            {
                throw new ArgumentException("El año debe ser mayor o igual a 1.", nameof(year));
            }

            if (weekOfYear < 1 || weekOfYear > 53)
            {
                throw new ArgumentException("El número de la semana debe estar entre 1 y 53.", nameof(weekOfYear));
            }
        }

        /// <summary>
        /// Obtiene la fecha del primer día de una semana específica en un año dado.
        /// </summary>
        /// <param name="year">El año para el cual se calculará la semana.</param>
        /// <param name="weekOfYear">El número de la semana dentro del año (basado en la cultura actual).</param>
        /// <returns>La fecha correspondiente al primer día de la semana especificada.</returns>
        /// <remarks>
        /// Este método utiliza las reglas de la cultura actual para determinar la primera semana del año.
        /// La primera semana se define como aquella que contiene al menos cuatro días.
        /// </remarks>
        public DateTime GetFirstDateOfWeek(int year, int weekOfYear)
        {
            ValidateYearAndWeek(year, weekOfYear);

            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        /// <summary>
        /// Obtiene la fecha del último día de una semana, dado el primer día de esa semana.
        /// </summary>
        /// <param name="weekOfYear">El número de la semana dentro del año (basado en la cultura actual).</param>
        /// <param name="year">El año para el cual se calculará la semana.</param>
        /// <returns>La fecha correspondiente al último día de la semana (6 días después del primer día).</returns>
        public DateTime GetLastDateOfWeek(int year, int weekOfYear)
        {
            ValidateYearAndWeek(year, weekOfYear);
            DateTime firstDateOfWeek = GetFirstDateOfWeek(year, weekOfYear);
            DateTime lastDateOfWeek = firstDateOfWeek.AddDays(6);
            return lastDateOfWeek;
        }
    }
}
