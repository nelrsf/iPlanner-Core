using iPlanner.Application.DTO.Teams;

namespace iPlanner.Application.Services
{
    public interface ITeamScheduleService
    {
        Task<IEnumerable<int>> GetAvailableYears();
        Task<IEnumerable<int>> GetAvailableWeeks(int year);
        Task<ScheduleTeamDataDTO> GetScheduleData(int year, int week);
    }
}