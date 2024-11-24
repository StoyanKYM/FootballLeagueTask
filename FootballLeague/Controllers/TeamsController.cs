using FootballLeague.Data.Data;
using FootballLeague.Data.Entities;
using FootballLeague.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly FootballLeagueDbContext _context;

        public TeamsController(FootballLeagueDbContext context)
        {
            _context = context;
        }

        // GET: api/teams/{id}
        [HttpGet("{id}")]
        public ActionResult<Team> GetTeamById(int id)
        {
            var team = _context.Teams.FirstOrDefault(m => m.Id == id);

            if (team == null)
            {
                return NotFound($"Team with id {id} not found.");
            }

            return team;
        }

        [HttpPost]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            try
            {
                if (team == null)
                {
                    return BadRequest("Invalid team data.");
                }

                _context.Teams.Add(team);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, "Database error occurred: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeam(int id, [FromBody] Team updatedTeam)
        {
            try
            {
                if (updatedTeam == null || id != updatedTeam.Id)
                {
                    return BadRequest("Invalid team data or ID mismatch.");
                }

                var existingTeam = _context.Teams.Find(id);

                if (existingTeam == null)
                {
                    return NotFound("Team not found.");
                }

                existingTeam.Name = updatedTeam.Name;

                _context.Teams.Update(existingTeam);
                _context.SaveChanges();

                return Ok(existingTeam);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, "A database error occurred. Please try again later.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            try
            {
                var team = _context.Teams.FirstOrDefault(t => t.Id == id);

                if (team == null)
                {
                    return NotFound("Team not found.");
                }
                
                _context.Teams.Remove(team);
                _context.SaveChanges();

                return Ok($"Team with ID {id} has been deleted.");
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database error while updating the match: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetRankings")]
        public IActionResult GetRankings()
        {
            var teams = _context.Teams
                .OrderByDescending(t => t.Points)
                .ToList();

            return Ok(teams);
        }

    }

}
