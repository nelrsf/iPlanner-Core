using iPlanner.Application.DTO;

namespace iPlanner.Application.Services
{
    public interface ILocationService
    {
        public Task<ICollection<LocationItemsDTO>> GetLocations();
    }
}
