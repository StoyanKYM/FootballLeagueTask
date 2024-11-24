using FootballLeague.Data.Data;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly FootballLeagueDbContext _context;

        public HomeController(FootballLeagueDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var teams = _context.Teams
                .OrderByDescending(t => t.Points)
                .ToList();

            return View(teams);
        }


    }
}
