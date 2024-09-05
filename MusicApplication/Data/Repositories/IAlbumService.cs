using MusicApplication.Models;

namespace MusicApplication.Data.Repositories
{
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAllAsync();
        Task<Album> GetByIdAsync(int id);
        Task AddAsync(Album album);
        Task<Album> UpdateAsync(int id, Album album);
        Task DeleteAsync(int id);
    }
}
