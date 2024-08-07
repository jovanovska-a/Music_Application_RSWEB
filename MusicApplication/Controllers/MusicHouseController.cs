using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApplication.Data;

namespace MusicApplication.Controllers
{
    public class MusicHouseController : Controller
    {
        private readonly AppDbContext _context;

        public MusicHouseController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var music_houses = _context.MusicHouses.ToListAsync();
            return View(music_houses);
        }
    }
}
