using iPlanner.Application.DTO.Teams;
using iPlanner.Application.Interfaces;
using iPlanner.Entities.Teams;
using iPlanner.Infrastructure.Common;

namespace iPlanner.Application.Services.Teams
{
    public class TeamMembersService : ITeamMembersService
    {
        private IMapper<TeamMemberDTO, TeamMember> _teamMemberMapper;
        public TeamMembersService(IMapper<TeamMemberDTO, TeamMember> memberMapper)
        {
            _teamMemberMapper = memberMapper;
        }

        public TeamMemberDTO CreateTeamMember()
        {
            TeamMember newMember = new TeamMember();
            newMember.Id = IdGenerator.GenerateUUID();
            return _teamMemberMapper.ToDTO(newMember);
        }
    }
}
