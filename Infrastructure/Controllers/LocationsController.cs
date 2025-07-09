using iPlanner.Application.DTO;
using iPlanner.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace iPlanner.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Obtiene todas las ubicaciones disponibles.
        /// </summary>
        /// <returns>Una colección de ubicaciones.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _locationService.GetLocations();
            return Ok(locations);
        }
    }
}
