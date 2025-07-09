using iPlanner.Application.DTO.Teams;

namespace iPlanner.Application.Interfaces
{
    public interface ITeamService
    {
        public Task<ICollection<TeamDTO>> GetAll();

        public void AddTeam(TeamDTO team);

        public void RemoveTeams(ICollection<TeamDTO> teams);

        public void UpdateTeam(TeamDTO team);

    }
}
