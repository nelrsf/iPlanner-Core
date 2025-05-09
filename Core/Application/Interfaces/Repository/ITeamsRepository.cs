using iPlanner.Core.Application.DTO.Teams;

namespace iPlanner.Core.Application.Interfaces.Repository
{
    public interface ITeamsRepository
    {
        public Task<ICollection<TeamDTO>> GetTeams();
        public Task AddTeam(TeamDTO team);
        public Task UpdateTeam(TeamDTO updatedTeam);
        public Task DeleteTeams(ICollection<TeamDTO> teams);
    }
}
