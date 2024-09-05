using MusicApplication.Models;

namespace MusicApplication.Data.Repositories
{
    public interface IMusicHouseService
    {
        Task<IEnumerable<MusicHouse>> GetAllAsync();
        Task<MusicHouse> GetByIdAsync(int id);
        Task AddAsync(MusicHouse music_house);
        Task<MusicHouse> UpdateAsync(int id, MusicHouse music_house);
        Task DeleteAsync(int id);
    }
}
