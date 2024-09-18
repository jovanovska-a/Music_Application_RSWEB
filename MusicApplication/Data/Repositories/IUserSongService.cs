using MusicApplication.Models;

namespace MusicApplication.Data.Repositories
{
    public interface IUserSongService
    {
        Task<List<Song>> GetAllUserSongs();
        string GetCurrentUserId();
        string GetCurrentUserUsername();
        bool HasSong(int songId);
        Task<AppUser> GetUserById(string id);
        bool Add(User_Song userSongs);
        bool Update(User_Song userSongs);
        bool Delete(User_Song userSongs);
        bool Save();
        User_Song GetUserSong(int id);
        bool SongExists(int id);
    }
}
