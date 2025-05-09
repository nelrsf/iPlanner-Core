using iPlanner.Core.Application.DTO.Calendar;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Services.Calendar;

namespace iPlanner_Core.Infrastructure.Repositories.Calendar
{
    public class FileCalendarRepository : ICalendarRepository
    {
        private readonly IFileService _fileService;
        private readonly string _calendarFilePath;

        public FileCalendarRepository(IFileService fileService)
        {
            _fileService = fileService;
            _calendarFilePath = _fileService.GetDataFilePath("calendar.json");
        }

        public async Task<CalendarDayDTO> GetCalendarDay(DateTime date)
        {
            var calendarDays = await LoadCalendarDaysAsync();
            return calendarDays.FirstOrDefault(cd => cd.Date.Date == date.Date);
        }

        public async Task<bool> UpsertCalendarDay(CalendarDayDTO calendarDay)
        {
            var calendarDays = await LoadCalendarDaysAsync();
            var existingDay = calendarDays.FirstOrDefault(cd => cd.Date.Date == calendarDay.Date.Date);

            if (existingDay != null)
            {
                calendarDays.Remove(existingDay);
            }

            calendarDays.Add(calendarDay);
            await SaveCalendarDaysAsync(calendarDays);

            return true;
        }

        private async Task<List<CalendarDayDTO>> LoadCalendarDaysAsync()
        {
            return await Task.Run(() =>
                _fileService.LoadJsonData<List<CalendarDayDTO>>(_calendarFilePath) ?? new List<CalendarDayDTO>());
        }

        private async Task SaveCalendarDaysAsync(List<CalendarDayDTO> calendarDays)
        {
            await Task.Run(() =>
                _fileService.SaveJsonData(_calendarFilePath, calendarDays));
        }
    }
}
