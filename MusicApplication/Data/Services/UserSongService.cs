using Microsoft.EntityFrameworkCore;
using MusicApplication.Data.Repositories;
using MusicApplication.Models;
using System.Net;
using System.Security.Claims;

namespace MusicApplication.Data.Services
{
    public class UserSongService : IUserSongService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserSongService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool Add(User_Song userSongs)
        {
            _context.User_Song.Add(userSongs);
            return Save();
        }

        public bool Delete(User_Song userSongs)
        {
            _context.User_Song.Remove(userSongs);
            return Save();
        }

        public async Task<List<Song>> GetAllUserSongs()
        {
            var curUser = GetCurrentUserId();
            List<User_Song> userSongs = await _context.User_Song.Where(u => u.AppUser == curUser).ToListAsync();
            List<Song> mySongs = new List<Song>();
            foreach (var song in userSongs)
            {
                var mySong = await _context.Songs.Include(a => a.Album).Include(r => r.MusicHouse).Include(bg => bg.Artists_Songs).ThenInclude(g => g.Artist).FirstOrDefaultAsync(i => i.Id == song.SongId);
                mySongs.Add(mySong);
            }
            return mySongs;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string GetCurrentUserUsername()
        {
            var curUser = GetCurrentUserId();
            var username = _context.Users.FirstOrDefault(i => i.Id == curUser).UserName;
            return username;
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public User_Song GetUserSong(int id)
        {
            var curUser = GetCurrentUserId();
            User_Song userSong = _context.User_Song.FirstOrDefault(a => a.AppUser == curUser && a.SongId == id);
            return userSong;
        }

        public bool HasSong(int songId)
        {
            var curUser = GetCurrentUserId();
            if (curUser == null)
            {
                var hasSong = false;
                return hasSong;
            }
            else
            {
                var hasSong = _context.User_Song.FirstOrDefault(u => u.AppUser == curUser && u.SongId == songId);
                return hasSong != null ? true : false;
            }
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SongExists(int id)
        {
            var curUser = GetCurrentUserId();
            User_Song userSong = _context.User_Song.FirstOrDefault(a => a.AppUser == curUser && a.SongId == id);
            return userSong != null ? true : false;
        }

        public bool Update(User_Song userSongs)
        {
            _context.User_Song.Update(userSongs);
            return Save();
        }
    }
}
