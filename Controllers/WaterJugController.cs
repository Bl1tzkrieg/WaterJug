using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterJug.Data;
using WaterJug.Models.Request;
using WaterJug.Services;

namespace WaterJug.Controllers
{
    /// <summary>
    /// Handles water jug challenge operations, including solving and getting solution history.
    /// </summary>
    public class WaterJugController : ControllerBase
    {
        private readonly IWaterJugService _waterJugService;
        private readonly AppDbContext _context;


        public WaterJugController(IWaterJugService waterJugService, AppDbContext context)
        {
            _waterJugService = waterJugService;
            _context = context;
        }


        /// <summary>
        /// Solves a water jug problem based on the provided capacities and target value.
        /// </summary>
        /// <param name="dto">DTO containing jug capacities and the target value.</param>
        /// <returns>
        /// 200 OK with a solution result if solvable; 400 BadRequest otherwise or if validation fails.
        /// </returns>
        [HttpPost("/api/waterjugChallenge")]
        public async Task<IActionResult> Solve([FromBody] WaterJugDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _waterJugService.SolveProblem(dto.Jug1Capacity, dto.Jug2Capacity, dto.Target);

            return result.success ? Ok(result) : BadRequest(result);
        }


        /// <summary>
        /// Retrieves a list of previously solved water jug problems if they exist.
        /// </summary>
        /// <returns>
        /// 200 OK with records or a message indicating no history is available.
        /// </returns>
        [HttpGet("/api/history")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _context.History
                .OrderByDescending(h => h.SolvedAt)
                .ToListAsync();

            if (!history.Any())
            {
                return Ok(new
                {
                    message = "No history records found.",
                    data = new List<object>()
                });
            }

            return Ok(new
            {
                message = "History records retrieved successfully.",
                data = history
            });
        }
    }
}
