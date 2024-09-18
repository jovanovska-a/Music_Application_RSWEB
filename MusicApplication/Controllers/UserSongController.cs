using Microsoft.AspNetCore.Mvc;
using MusicApplication.Data.Repositories;
using MusicApplication.Models;

namespace MusicApplication.Controllers
{
    public class UserSongController : Controller
    {
        private readonly IUserSongService _userSongService;

        public UserSongController(IUserSongService userSongService)
        {
            _userSongService = userSongService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Song> mySongs = await _userSongService.GetAllUserSongs();
            return View(mySongs);
        }
        public async Task<IActionResult> DownloadSong(int id)
        {
            var curUser = _userSongService.GetCurrentUserId();
            bool isSong = _userSongService.SongExists(id);
            if (isSong)
            {
                return Redirect("/Song/Index");
            }
            User_Song newUS = new User_Song()
            {
                AppUser = curUser,
                SongId = id
            };
            _userSongService.Add(newUS);
            return RedirectToAction("Index");   
        }
        public async Task<IActionResult> Delete(int id)
        {
            var userSong = _userSongService.GetUserSong(id);
            _userSongService.Delete(userSong);
            return RedirectToAction("Index");
        }
    }
}
