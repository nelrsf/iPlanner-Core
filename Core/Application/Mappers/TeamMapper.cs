using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Teams;

namespace iPlanner.Core.Application.Mappers
{
    public class TeamMemberMapper : IMapper<TeamMemberDTO, TeamMember>
    {
        public TeamMemberDTO ToDTO(TeamMember entity)
        {
            TeamMemberDTO dto = new TeamMemberDTO(entity.Id, entity.Name);
            dto.Position = entity.Position;
            return dto;
        }

        public TeamMember ToEntity(TeamMemberDTO dto)
        {
            TeamMember entity = new TeamMember();
            entity.Id = dto.Id;
            entity.Name = dto.Name;
            entity.Position = dto.Position;
            return entity;
        }
    }

    public class TeamMapper : IMapper<TeamDTO, Team>
    {
        private IMapper<TeamMemberDTO, TeamMember> _teamMemberMapper;

        public TeamMapper(IMapper<TeamMemberDTO, TeamMember> mapper)
        {
            _teamMemberMapper = mapper;
        }
        public TeamDTO ToDTO(Team entity)
        {
            if (entity == null) return null;

            TeamDTO dto = new TeamDTO(entity.Id, entity.Name);
            dto.Leader = _teamMemberMapper.ToDTO(entity.Leader);
            dto.Description = entity.Description;
            if (entity.Members == null)
            {
                dto.Members = new List<TeamMemberDTO>();
            }
            else
            {
                dto.Members = new List<TeamMemberDTO>(
                        entity.Members.Select(tm => _teamMemberMapper.ToDTO(tm))
                    );
            }
            return dto;

        }

        public Team ToEntity(TeamDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            Team entity = new Team()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Leader = _teamMemberMapper.ToEntity(dto.Leader)
            };

            if (dto.Members == null)
            {
                entity.Members = new List<TeamMember>();
            }
            else
            {
                entity.Members = new List<TeamMember>(
                        dto.Members.Select(tm => _teamMemberMapper.ToEntity(tm))
                    );
            }
            return entity;
        }
    }
}
