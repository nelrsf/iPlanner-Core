using iPlanner.Application.DTO.Orders;
using iPlanner.Application.DTO.Teams;
using iPlanner.Entities.Locations;


namespace iPlanner.Application.DTO
{
    public class ReportsDTO
    {
        private string _reportId;
        private TeamDTO _team;
        private DateTime? _date;
        private TimeSpan? _timeInit;
        private TimeSpan? _timeEnd;
        private ICollection<ActivitiesDTO>? _activities;
        private ICollection<ReportCustomMemberDTO>? _activeMembers;
        private OrderDTO? _order;

        public OrderDTO? Order
        {
            get => _order;
            set
            {
                _order = value;
            }
        }

        public ICollection<ReportCustomMemberDTO>? ActiveMembers
        {
            get => _activeMembers;
            set
            {
                _activeMembers = value;
            }
        }

        public string ReportId
        {
            get => _reportId;
            set
            {
                _reportId = value;
            }
        }

        public TeamDTO Team
        {
            get => _team;
            set
            {
                _team = value;
            }
        }

        public DateTime? Date
        {
            get => _date;
            set
            {
                _date = value;
            }
        }

        public TimeSpan? TimeInit
        {
            get => _timeInit;
            set
            {
                _timeInit = value;
            }
        }

        public TimeSpan? TimeEnd
        {
            get => _timeEnd;
            set
            {
                _timeEnd = value;
            }
        }

        public ICollection<ActivitiesDTO> Activities
        {
            get => _activities;
            set
            {
                _activities = value;
            }
        }

    }

    public class ActivitiesDTO
    {
        private ICollection<LocationItemsDTO> _locations;
        private string _description;

        public ICollection<LocationItemsDTO> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
            }
        }

        public ActivitiesDTO()
        {
            Locations = new List<LocationItemsDTO>();
        }

    }

    public class LocationItemsDTO
    {
        private int _id;
        private string _name;
        private string _icon;
        private LocationTypes _locationType;
        private LocationItemsDTO? _parent;
        private ICollection<LocationItemsDTO> _children;
        public string? SAPEquipment {  get; set; }
        public string? SAPTechnicalLocation { get; set; }


        public LocationItemsDTO? Parent
        {
            get => _parent;
            set
            {
                _parent = value;
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
            }
        }

        public ICollection<LocationItemsDTO> Children
        {
            get => _children;
            set
            {
                _children = value;
            }
        }

        public LocationTypes LocationType
        {
            get => _locationType;
            set
            {
                _locationType = value;
            }
        }

        public LocationItemsDTO(LocationItem location)
        {
            Children = new List<LocationItemsDTO>();

            if (location != null)
            {
                Id = location.LocationId;
                Name = location._name;
                Icon = location.Icon;
                LocationType = location.LocationType;

                if (location.Children != null && location.Children.Count > 0)
                {
                    foreach (var child in location.Children)
                    {
                        Children.Add(new LocationItemsDTO(child));
                    }
                }
            }
        }

        public LocationItemsDTO(int id, string name, string icon, LocationTypes type)
        {
            Id = id;
            Name = name;
            Icon = icon;
            LocationType = type;
            Children = new List<LocationItemsDTO>();
        }

        public LocationItemsDTO()
        {
            Children = new List<LocationItemsDTO>();
        }

    }

    public class ReportCustomMemberDTO
    {
        public ReportCustomMemberDTO(bool isActive, TeamMemberDTO member)
        {
            IsActive = isActive;
            Member = member;
        }

        public bool IsActive { get; set; }
        public TeamMemberDTO Member { get; set; }
    }
}
