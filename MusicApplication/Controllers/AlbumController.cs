using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApplication.Data;

namespace MusicApplication.Controllers
{
    public class AlbumController : Controller
    {
        private readonly AppDbContext _context;

        public AlbumController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var albums = _context.Albums.ToListAsync();
            return View(albums);
        }
    }
}
