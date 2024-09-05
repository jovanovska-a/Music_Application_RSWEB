using MusicApplication.Models;

namespace MusicApplication.Data.Repositories
{
    public interface IArtistSongService
    {
        Task<IEnumerable<Artist_Song>> GetAllAsync();
        void Add(Artist_Song artistSong);
        Task<Artist_Song> UpdateAsync(int id, Artist_Song artistSong);
        bool Delete(Artist_Song artistSong);
        bool Save();
        Task DeleteBySongIdAsync(int id);
        Task<IEnumerable<int>> GetSongByArtistIdAsync(int id);
    }

}
