using FootballLeague.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Data.Data
{
    public  class FootballLeagueDbContext : DbContext
    {
        public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options)
            : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Match>().HasOne(m => m.HomeTeam).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Match>().HasOne(m => m.AwayTeam).WithMany().OnDelete(DeleteBehavior.Restrict);

            // Seed initial teams
            builder.Entity<Team>().HasData(
            new Team { Id = 1, Name = "Manchester United", Points = 7, MatchesPlayed = 3 },
            new Team { Id = 2, Name = "Liverpool", Points = 1, MatchesPlayed = 3 }
        );

            // Seed initial matches
            builder.Entity<Match>().HasData(
                new Match
                {
                    Id = 1,
                    HomeTeamId = 1, // Manchester United
                    AwayTeamId = 2, // Liverpool
                    HomeTeamScore = 3,
                    AwayTeamScore = 1,
                    IsPlayed = true,
                },
                new Match
                {
                    Id = 2,
                    HomeTeamId = 1, // Manchester United
                    AwayTeamId = 2, // Liverpool
                    HomeTeamScore = 2,
                    AwayTeamScore = 2,
                    IsPlayed = true,
                },
                new Match
                {
                    Id = 3,
                    HomeTeamId = 1, // Manchester United
                    AwayTeamId = 2, // Liverpool
                    HomeTeamScore = 4,
                    AwayTeamScore = 1,
                    IsPlayed = true,
                }
            );
        }  

    }
}
