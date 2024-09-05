using MusicApplication.Models;

namespace MusicApplication.Data.Repositories
{
    public interface ISongService
    {
        Task<IEnumerable<Song>> GetAllAsync();
        Task<Song> GetByIdAsync(int id);
        Task<Song> GetByIdAsyncNoTracking(int id);
        Task AddAsync(Song song);
        Task<Song> UpdateAsync(int id, Song song);
        Task DeleteAsync(int id);
        Task<Song>GetLastSong();
    }
}
