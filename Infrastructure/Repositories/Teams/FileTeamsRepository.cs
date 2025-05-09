using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Interfaces.Repository;
using iPlanner.Infrastructure.Common;

namespace iPlanner_Core.Infrastructure.Repositories.Teams
{
    public class FileTeamsRepository : ITeamsRepository
    {
        private IFileService _fileService;
        private readonly string _teamsFilePath;
        private ICollection<TeamDTO>? _teams;

        public FileTeamsRepository(IFileService fileService)
        {
            _fileService = fileService;
            _teamsFilePath = _fileService.GetDataFilePath("Teams.json");
            _fileService.EnsureDirectoryExists(_teamsFilePath);
            LoadTeams();
        }


        private async void LoadTeams()
        {
            _teams = await GetTeams() ?? new List<TeamDTO>();
        }


        public async Task AddTeam(TeamDTO team)
        {
            if (_teams == null)
            {
                throw new ArgumentNullException(nameof(_teams));
            }

            await Task.Run(() =>
            {
                team.Id = IdGenerator.GenerateUUID();
                _teams.Add(team);
                SaveTeams();
            });
        }

        public async Task DeleteTeams(ICollection<TeamDTO> teamsToRemove)
        {
            await Task.Run(() =>
            {
                if (_teams == null)
                {
                    throw new ArgumentNullException(nameof(_teams));
                }

                List<TeamDTO> tempTeams = new List<TeamDTO>();

                foreach (TeamDTO team in teamsToRemove)
                {
                    TeamDTO? teamToRemove = FindTeamById(team.Id);
                    if (teamToRemove != null)
                    {
                        tempTeams.Add(teamToRemove);
                    }
                }

                foreach (TeamDTO teamToRemove in tempTeams)
                {
                    _teams.Remove(teamToRemove);
                }

                SaveTeams();
            });
        }

        public async Task<ICollection<TeamDTO>> GetTeams()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _fileService.LoadJsonData<List<TeamDTO>>(_teamsFilePath) ?? new List<TeamDTO>();
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading teams", ex);
            }

        }

        public async Task UpdateTeam(TeamDTO updatedTeam)
        {
            if (_teams == null)
            {
                throw new ArgumentNullException(nameof(_teams));
            }

            await Task.Run(() =>
            {
                TeamDTO? existingTeam = FindTeamById(updatedTeam.Id);
                if (existingTeam != null)
                {
                    existingTeam.Name = updatedTeam.Name;
                    existingTeam.Description = updatedTeam.Description;
                    existingTeam.Leader = updatedTeam.Leader;
                    existingTeam.Members = updatedTeam.Members;
                    SaveTeams();
                }
                else
                {
                    throw new Exception("Team not found");
                }
            });
        }

        private void SaveTeams()
        {
            _fileService.SaveJsonData(_teamsFilePath, _teams);
        }

        private TeamDTO? FindTeamById(string? teamId)
        {
            if (_teams == null || teamId == null) return null;
            return _teams.FirstOrDefault(t => t.Id == teamId);
        }
    }
}
