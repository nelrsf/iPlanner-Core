using System.Globalization;

namespace iPlanner.Application.Services.Calendar
{
    /// <summary>
    /// Proporciona m�todos para realizar c�lculos relacionados con fechas en un calendario,
    /// como obtener el primer y �ltimo d�a de una semana espec�fica.
    /// </summary>
    public class CalendarDateCalculator
    {
        /// <summary>
        /// Valida los par�metros de a�o y n�mero de semana.
        /// </summary>
        /// <param name="year">El a�o a validar.</param>
        /// <param name="weekOfYear">El n�mero de la semana a validar.</param>
        /// <exception cref="ArgumentException">Se lanza si los par�metros no son v�lidos.</exception>
        private void ValidateYearAndWeek(int year, int weekOfYear)
        {
            if (year < 1)
            {
                throw new ArgumentException("El a�o debe ser mayor o igual a 1.", nameof(year));
            }

            if (weekOfYear < 1 || weekOfYear > 53)
            {
                throw new ArgumentException("El n�mero de la semana debe estar entre 1 y 53.", nameof(weekOfYear));
            }
        }

        /// <summary>
        /// Obtiene la fecha del primer d�a de una semana espec�fica en un a�o dado.
        /// </summary>
        /// <param name="year">El a�o para el cual se calcular� la semana.</param>
        /// <param name="weekOfYear">El n�mero de la semana dentro del a�o (basado en la cultura actual).</param>
        /// <returns>La fecha correspondiente al primer d�a de la semana especificada.</returns>
        /// <remarks>
        /// Este m�todo utiliza las reglas de la cultura actual para determinar la primera semana del a�o.
        /// La primera semana se define como aquella que contiene al menos cuatro d�as.
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
        /// Obtiene la fecha del �ltimo d�a de una semana, dado el primer d�a de esa semana.
        /// </summary>
        /// <param name="weekOfYear">El n�mero de la semana dentro del a�o (basado en la cultura actual).</param>
        /// <param name="year">El a�o para el cual se calcular� la semana.</param>
        /// <returns>La fecha correspondiente al �ltimo d�a de la semana (6 d�as despu�s del primer d�a).</returns>
        public DateTime GetLastDateOfWeek(int year, int weekOfYear)
        {
            ValidateYearAndWeek(year, weekOfYear);
            DateTime firstDateOfWeek = GetFirstDateOfWeek(year, weekOfYear);
            DateTime lastDateOfWeek = firstDateOfWeek.AddDays(6);
            return lastDateOfWeek;
        }
    }
}
