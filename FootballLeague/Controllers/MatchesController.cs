using System.Text.RegularExpressions;
using FootballLeague.Data.Data;
using FootballLeague.Data.Entities;
using FootballLeague.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Match = FootballLeague.Data.Entities.Match;

namespace FootballLeague.Controllers
{
    // Controllers/MatchesController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly FootballLeagueService _footballLeagueService;
        private readonly FootballLeagueDbContext _context;

        public MatchesController(FootballLeagueDbContext context, FootballLeagueService footballLeagueService)
        {
            _context = context;
            _footballLeagueService = footballLeagueService;
        }

        [HttpGet("GetAllMatches")]
        public IActionResult GetAllMatches()
        {
            // Retrieve all matches from the database, including related teams
            var matches = _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToList();

            if (matches == null || !matches.Any())
            {
                return NotFound("No matches found.");
            }

            return Ok(matches);
        }

        // GET: api/matches/{id}
        [HttpGet("{id}")]
        public IActionResult GetMatchById(int id)
        {
            var match = _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .FirstOrDefault(m => m.Id == id);

            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        [HttpPost("CreateMatch")]
        public IActionResult CreateMatch([FromBody] Match match)
        {
            try
            {
                if (match == null)
                {
                    return BadRequest("Invalid match data.");
                }

                var homeTeam = _context.Teams.Find(match.HomeTeam.Id);
                var awayTeam = _context.Teams.Find(match.AwayTeam.Id);

                if (homeTeam == null || awayTeam == null)
                {
                    return BadRequest("One or both teams not found.");
                }

                // Assign the teams to the match object (navigation properties)
                match.HomeTeam = homeTeam;
                match.AwayTeam = awayTeam;

                // Update points only if the match has been played
                if (match.IsPlayed)
                {
                    homeTeam.MatchesPlayed++;
                    awayTeam.MatchesPlayed++;
                    _footballLeagueService.AddPoints(match);
                }

                _context.Matches.Add(match);
                _context.SaveChanges();

                // _footballLeagueService.AddPoints(match);

                return Ok(match);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, "Database update error: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMatch(int id, [FromBody] Match updatedMatch)
        {
            try
            {
                if (updatedMatch == null || id != updatedMatch.Id)
                {
                    return BadRequest("Invalid match data or ID mismatch.");
                }

                var existingMatch = _context.Matches
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .FirstOrDefault(m => m.Id == id);

                if (existingMatch == null)
                {
                    return NotFound("Match not found.");
                }

                if (!existingMatch.IsPlayed)
                {
                    return BadRequest("Cannot update a match that has not been played.");
                }

                _footballLeagueService.SubtractPoints(existingMatch);

                // Update the match details
                existingMatch.HomeTeamScore = updatedMatch.HomeTeamScore;
                existingMatch.AwayTeamScore = updatedMatch.AwayTeamScore;

                // Add points to the rankings based on the updated match data
                _footballLeagueService.AddPoints(existingMatch);

                _context.Matches.Update(existingMatch);
                _context.SaveChanges();


                return Ok(existingMatch);
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update errors
                return StatusCode(500, $"Database error while updating the match: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle general errors
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteMatch(int id)
        {
            try
            {
                var match = _context.Matches.FirstOrDefault(m => m.Id == id);

                if (match == null)
                {
                    return NotFound("Match not found.");
                }

                _context.Matches.Remove(match);
                _context.SaveChanges();

                _footballLeagueService.SubtractPoints(match);


                return Ok($"Match with ID {id} has been deleted.");
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
    }

}
