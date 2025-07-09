using iPlanner.Application.DTO.Teams;
using iPlanner.Application.Interfaces;
using iPlanner.Entities.Teams;

namespace iPlanner.Application.Mappers
{
    public class ScheduleTeamDataMapper : IMapper<ScheduleTeamDataDTO, ScheduleTeamData>
    {
        private IMapper<TeamDTO, Team> _teamMapper;
        public ScheduleTeamDataMapper(IMapper<TeamDTO, Team> teamMapper) {
            _teamMapper = teamMapper;
        }
        public ScheduleTeamDataDTO ToDTO(ScheduleTeamData entity)
        {
            return new ScheduleTeamDataDTO
            {
                TeamItems = entity.TeamItems.Select(item => new ScheduleTeamItemDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    TotalHours = item.TotalHours,
                    CompletionPercentage = item.CompletionPercentage,
                    HasConflicts = item.HasConflicts
                }).ToList(),
                ConflictItems = entity.ConflictItems.Select(conflict => new ConflictItemDTO
                {
                    Team = _teamMapper.ToDTO(conflict.Team),
                    Date = conflict.Date,
                    Description = conflict.Description
                }).ToList()
            };
        }

        public ScheduleTeamData ToEntity(ScheduleTeamDataDTO dto)
        {
            return new ScheduleTeamData
            {
                TeamItems = dto.TeamItems.Select(item => new ScheduleTeamItem(new Team
                {
                    Id = item.Id,
                    Name = item.Name
                })
                {
                    TotalHours = item.TotalHours,
                    HasConflicts = item.HasConflicts
                }).ToList(),
                ConflictItems = dto.ConflictItems.Select(conflict => new ConflictItem
                {
                    Team = _teamMapper.ToEntity(conflict.Team),
                    Date = conflict.Date,
                    Description = conflict.Description
                }).ToList()
            };
        }
    }
}
