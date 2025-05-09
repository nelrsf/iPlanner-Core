using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Interfaces.Repository;

namespace iPlanner.Core.Application.Services.Teams
{
    public class TeamsService : ITeamService
    {
        private ITeamsRepository _teamsRepository;

        public TeamsService(ITeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;

            InitializeTeams();
        }

        private async void InitializeTeams()
        {
            ICollection<TeamDTO> teams = await _teamsRepository.GetTeams();
        }


        public async Task<ICollection<TeamDTO>> GetAll()
        {
            return await _teamsRepository.GetTeams();
        }

        public async void AddTeam(TeamDTO team)
        {
            await _teamsRepository.AddTeam(team);
        }

        public async void RemoveTeams(ICollection<TeamDTO> teamsToRemove)
        {
            await _teamsRepository.DeleteTeams(teamsToRemove);
        }

        public async void UpdateTeam(TeamDTO team)
        {
            await _teamsRepository.UpdateTeam(team);
        }
    }
}