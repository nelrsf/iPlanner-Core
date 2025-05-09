using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Orders;
using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Locations;
using iPlanner.Core.Entities.Reports;
using iPlanner.Core.Entities.Teams;
using static iPlanner.Core.Entities.Reports.ReportCustomTeam;

namespace iPlanner.Core.Application.Mappers
{
    public class LocationTypeDTO
    {
        public static LocationTypes substation = LocationTypes.Substation;
        public static LocationTypes network = LocationTypes.Network;
        public static LocationTypes structure = LocationTypes.Structure;
        public static LocationTypes equipment = LocationTypes.Equipmen;
        public static LocationTypes treeNode = LocationTypes.TreeNode;

    }
    public class LocationItemsMapper : IMapper<LocationItemsDTO, LocationItem>
    {
        public LocationItemsDTO ToDTO(LocationItem entity)
        {
            LocationItemsDTO dto = new LocationItemsDTO()
            {
                Name = entity._name,
                Icon = entity.Icon
            };

            if (entity.Children == null || entity.Children?.Count == 0)
            {
                dto.Children = new List<LocationItemsDTO>();
            }
            else
            {
                dto.Children = new List<LocationItemsDTO>(
                        entity.Children.Select(l => ToDTO(l))
                    );
            }
            return dto;
        }

        public LocationItem ToEntity(LocationItemsDTO dto)
        {
            LocationItem entity = new LocationItem(dto.Id, dto.Name, dto.Icon, dto.LocationType);
            if (dto.Children == null || dto.Children?.Count == 0)
            {
                entity.Children = new List<LocationItem>();
            }
            else
            {
                entity.Children = new List<LocationItem>(
                        dto.Children.Select(l => ToEntity(l))
                    );
            }
            return entity;
        }
    }
    public class ActivitiesMapper : IMapper<ActivitiesDTO, Activity>
    {
        private IMapper<LocationItemsDTO, LocationItem> _locationMapper;
        public ActivitiesMapper(IMapper<LocationItemsDTO, LocationItem> locationItemMapper)
        {
            _locationMapper = locationItemMapper;
        }
        public ActivitiesDTO ToDTO(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Error al mapear, entidad no puede ser null");
            }

            ActivitiesDTO dto = new ActivitiesDTO()
            {
                Description = entity.Description
            };

            if (dto.Locations == null)
            {
                dto.Locations = new List<LocationItemsDTO>();
            }
            else
            {
                dto.Locations = new List<LocationItemsDTO>(
                    entity.Locations.Select(l => _locationMapper.ToDTO(l))
                    );
            }
            return dto;
        }

        public Activity ToEntity(ActivitiesDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("Error al mapear, entidad no puede ser null");
            }
            Activity entity = new Activity()
            {
                Description = dto.Description
            };
            if (dto.Locations == null)
            {
                entity.Locations = new List<LocationItem>();
            }
            else
            {
                entity.Locations = new List<LocationItem>(
                        dto.Locations.Select(l => _locationMapper.ToEntity(l))
                    );
            }
            return entity;
        }
    }


    public class ReportsMapper : IMapper<ReportsDTO, Report>
    {
        private IMapper<ActivitiesDTO, Activity> _activityMapper;
        private IMapper<TeamDTO, Team> _teamMapper;
        private IMapper<TeamMemberDTO, TeamMember> _teamMemberMapper;
        private IMapper<OrderDTO, Order> _orderMapper;
        public ReportsMapper(IMapper<ActivitiesDTO, Activity> activityMapper,
                            IMapper<TeamDTO, Team> teamMapper,
                            IMapper<TeamMemberDTO, TeamMember> teamMemberMapper,
                            IMapper<OrderDTO, Order> orderMapper)
        {
            _activityMapper = activityMapper;
            _teamMapper = teamMapper;
            _teamMemberMapper = teamMemberMapper;
            _orderMapper = orderMapper;
        }
        public ReportsDTO ToDTO(Report entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Error al mapear, entidad no puede ser null");
            }

            ReportsDTO dto = new ReportsDTO()
            {
                ReportId = entity.ReportId,
                Date = entity.Date,
                TimeEnd = entity.TimeEnd,
                TimeInit = entity.TimeInit,
                Order = entity.Order != null ? _orderMapper.ToDTO(entity.Order) : null,
                ActiveMembers = new List<ReportCustomMemberDTO>(entity.CustomTeam.ActiveMembers.Select(
                    am => new ReportCustomMemberDTO(am.IsActive, _teamMemberMapper.ToDTO(am.TeamMember))
                    )
                )
            };


            if (entity.Team != null)
            {
                dto.Team = _teamMapper.ToDTO(entity.Team);
            }

            if (entity.Activities == null)
            {
                dto.Activities = new List<ActivitiesDTO>();
            }
            else
            {
                dto.Activities = new List<ActivitiesDTO>(
                        entity.Activities.Select(a => _activityMapper.ToDTO(a))
                    );
            }
            return dto;
        }

        public Report ToEntity(ReportsDTO dto)
        {
            Report report = new Report()
            {
                ReportId = dto.ReportId,
                Date = dto.Date,
                TimeEnd = dto.TimeEnd,
                TimeInit = dto.TimeInit,
                Order = dto.Order != null ? _orderMapper.ToEntity(dto.Order) : null,
            };
            if (dto.Team != null)
            {
                report.Team = _teamMapper.ToEntity(dto.Team);
            }

            if (dto.ActiveMembers != null)
            {
                report.CustomTeam.ActiveMembers = new List<ReportCustomMember>(
                    dto.ActiveMembers.Select(am => new ReportCustomMember(am.IsActive, _teamMemberMapper.ToEntity(am.Member)))
                );
            }

            if (dto.Activities == null)
            {
                report.Activities = new List<Activity>();
            }
            else
            {
                report.Activities = new List<Activity>(
                    dto.Activities.Select(a => _activityMapper.ToEntity(a))
                );
            }
            return report;
        }
    }
}
