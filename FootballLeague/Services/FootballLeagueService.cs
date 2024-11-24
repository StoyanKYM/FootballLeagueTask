using FootballLeague.Data.Data;
using FootballLeague.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Services
{
    public class FootballLeagueService
    {
        private readonly FootballLeagueDbContext _context;

        public FootballLeagueService(FootballLeagueDbContext context)
        {
            _context = context;
        }


        public void AddPoints(Match match)
        {
            var homeTeam = _context.Teams.Find(match.HomeTeamId);
            var awayTeam = _context.Teams.Find(match.AwayTeamId);

            if (homeTeam == null || awayTeam == null)
            {
                throw new ArgumentException("Invalid team IDs provided in the match.");
            }

            // Determine the winner and assign points
            if (match.HomeTeamScore > match.AwayTeamScore)
            {
               homeTeam.Points += 3;
            }
            else if (match.HomeTeamScore < match.AwayTeamScore)
            {
               awayTeam.Points += 3;
            }
            else
            {
               // Draw
               homeTeam.Points += 1;
               awayTeam.Points += 1;
            }

            _context.SaveChanges();
        }

        public void SubtractPoints(Match match)
        {
            var homeTeam = _context.Teams.Find(match.HomeTeamId);
            var awayTeam = _context.Teams.Find(match.AwayTeamId);

            if (homeTeam == null || awayTeam == null)
            {
                throw new ArgumentException("Invalid team IDs provided in the match.");
            }

            if (match.HomeTeamScore > match.AwayTeamScore)
            {
                homeTeam.Points -= 3;
            }
            else if (match.HomeTeamScore < match.AwayTeamScore)
            {
                awayTeam.Points -= 3;
            }
            else
            {
                homeTeam.Points -= 1;
                awayTeam.Points -= 1;
            }

            //if(homeTeam.MatchesPlayed == 0)
            //{
            //    homeTeam.MatchesPlayed = 0;
            //}
            //else
            //{
            //    homeTeam.MatchesPlayed--;
            //}

            //if (awayTeam.MatchesPlayed == 0)
            //{
            //    awayTeam.MatchesPlayed = 0;
            //}
            //else
            //{
            //    awayTeam.MatchesPlayed--;
            //}

            //if (homeTeam.Points < 0)
            //{
            //    homeTeam.Points = 0;
            //}

            //if (awayTeam.Points < 0)
            //{
            //    awayTeam.Points = 0;
            //}
            


            _context.SaveChanges();
        }

    }
}
