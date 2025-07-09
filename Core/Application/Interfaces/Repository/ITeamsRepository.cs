using iPlanner.Application.DTO.Teams;

namespace iPlanner.Application.Interfaces.Repository
{
    public interface ITeamsRepository
    {
        public Task<ICollection<TeamDTO>> GetTeams();
        public Task AddTeam(TeamDTO team);
        public Task UpdateTeam(TeamDTO updatedTeam);
        public Task DeleteTeams(ICollection<TeamDTO> teams);
    }
}
