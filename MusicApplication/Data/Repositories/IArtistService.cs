using MusicApplication.Models;

namespace MusicApplication.Data.Repositories
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist> GetByIdAsync(int id);
        Task AddAsync(Artist artist);
        Task<Artist> UpdateAsync(int id,Artist artist);
        Task DeleteAsync(int id);
    }
}
