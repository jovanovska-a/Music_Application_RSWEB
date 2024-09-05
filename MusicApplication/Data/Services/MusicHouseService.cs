using Microsoft.EntityFrameworkCore;
using MusicApplication.Data.Repositories;
using MusicApplication.Models;

namespace MusicApplication.Data.Services
{
    public class MusicHouseService : IMusicHouseService
    {
        private readonly AppDbContext _context;

        public MusicHouseService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(MusicHouse music_house)
        {
            _context.MusicHouses.AddAsync(music_house);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.MusicHouses.FirstOrDefaultAsync(n => n.Id == id);
            _context.MusicHouses.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MusicHouse>> GetAllAsync()
        {
            var result = await _context.MusicHouses.ToListAsync();
            return result;
        }

        public async Task<MusicHouse> GetByIdAsync(int id)
        {
            var result = await _context.MusicHouses.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<MusicHouse> UpdateAsync(int id, MusicHouse music_house)
        {
            _context.Update(music_house);
            await _context.SaveChangesAsync();
            return music_house;
        }
    }
}
