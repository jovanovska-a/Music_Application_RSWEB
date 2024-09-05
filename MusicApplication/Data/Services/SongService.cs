using Microsoft.EntityFrameworkCore;
using MusicApplication.Data.Repositories;
using MusicApplication.Models;

namespace MusicApplication.Data.Services
{
    public class SongService : ISongService
    {
        private readonly AppDbContext _context;

        public SongService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Song song)
        {
            _context.Songs.AddAsync(song);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Songs.FirstOrDefaultAsync(n => n.Id == id);
            _context.Songs.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Song>> GetAllAsync()
        {
            var result = await _context.Songs.Include(n => n.Album).Include(n => n.MusicHouse).Include(n => n.Artists_Songs).ThenInclude(a => a.Artist).ToListAsync();
            return result;
        }

        public async Task<Song> GetByIdAsync(int id)
        {
            var result = await _context.Songs.Include(n => n.Album).Include(n => n.MusicHouse).Include(n => n.Artists_Songs).ThenInclude(a => a.Artist).FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<Song> GetByIdAsyncNoTracking(int id)
        {
            var result = await _context.Songs.Include(n => n.Album).Include(n => n.MusicHouse).Include(n => n.Artists_Songs).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<Song> UpdateAsync(int id, Song song)
        {
            _context.Update(song);
            await _context.SaveChangesAsync();
            return song;
        }
        public async Task<Song> GetLastSong()
        {
            return await _context.Songs.AsNoTracking().FirstOrDefaultAsync(i => i.Id == _context.Songs.Max(i => i.Id));
        }
    }
}
