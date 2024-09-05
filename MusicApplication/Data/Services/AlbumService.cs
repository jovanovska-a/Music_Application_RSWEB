using Microsoft.EntityFrameworkCore;
using MusicApplication.Data.Repositories;
using MusicApplication.Models;

namespace MusicApplication.Data.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly AppDbContext _context;
        public AlbumService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Album album)
        {
            _context.Albums.AddAsync(album);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Albums.FirstOrDefaultAsync(n => n.Id == id);
            _context.Albums.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Album>> GetAllAsync()
        {
            var result = await _context.Albums.ToListAsync();
            return result;
        }

        public async Task<Album> GetByIdAsync(int id)
        {
            var result = await _context.Albums.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<Album> UpdateAsync(int id, Album album)
        {
            _context.Update(album);
            await _context.SaveChangesAsync();
            return album;
        }
    }
}
