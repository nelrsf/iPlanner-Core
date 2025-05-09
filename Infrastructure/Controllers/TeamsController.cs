using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Services.Teams;
using Microsoft.AspNetCore.Mvc;

namespace iPlanner.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly TeamsService _teamsService;

        public TeamsController(TeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        /// <summary>
        /// Obtiene todos los equipos.
        /// </summary>
        /// <returns>Una colección de equipos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _teamsService.GetAll();
            return Ok(teams);
        }

        /// <summary>
        /// Agrega un nuevo equipo.
        /// </summary>
        /// <param name="team">El equipo a agregar.</param>
        [HttpPost]
        public IActionResult AddTeam([FromBody] TeamDTO team)
        {
            _teamsService.AddTeam(team);
            return Ok();
        }

        /// <summary>
        /// Elimina equipos.
        /// </summary>
        /// <param name="teamsToRemove">Los equipos a eliminar.</param>
        [HttpDelete]
        public IActionResult RemoveTeams([FromBody] ICollection<TeamDTO> teamsToRemove)
        {
            _teamsService.RemoveTeams(teamsToRemove);
            return Ok();
        }

        /// <summary>
        /// Actualiza un equipo existente.
        /// </summary>
        /// <param name="team">El equipo a actualizar.</param>
        [HttpPut]
        public IActionResult UpdateTeam([FromBody] TeamDTO team)
        {
            _teamsService.UpdateTeam(team);
            return Ok();
        }
    }
}
