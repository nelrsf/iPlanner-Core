using iPlanner.Application.DTO;

namespace iPlanner.Application.Interfaces.Repository
{
    public interface ILocationsRepository
    {
        Task<ICollection<LocationItemsDTO>> GetLocations();
    }
}
