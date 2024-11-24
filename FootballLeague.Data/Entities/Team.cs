using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FootballLeague.Data.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Points { get; set; } = 0;

        public int MatchesPlayed { get; set; }
    }
}
