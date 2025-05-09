using iPlanner.Core.Application.DTO;

namespace iPlanner.Core.Application.Interfaces.Repository
{
    public interface ILocationsRepository
    {
        Task<ICollection<LocationItemsDTO>> GetLocations();
    }
}
