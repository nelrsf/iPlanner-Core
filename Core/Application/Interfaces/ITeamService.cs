using iPlanner.Core.Application.DTO.Teams;

namespace iPlanner.Core.Application.Interfaces
{
    public interface ITeamService
    {
        public Task<ICollection<TeamDTO>> GetAll();

        public void AddTeam(TeamDTO team);

        public void RemoveTeams(ICollection<TeamDTO> teams);

        public void UpdateTeam(TeamDTO team);

    }
}
