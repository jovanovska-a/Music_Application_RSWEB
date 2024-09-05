using Microsoft.EntityFrameworkCore;
using MusicApplication.Data.Repositories;
using MusicApplication.Models;

namespace MusicApplication.Data.Services
{
    public class ArtistService : IArtistService
    {
        private readonly AppDbContext _context;
        public ArtistService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Artist artist)
        {
            _context.Artists.AddAsync(artist);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Artists.FirstOrDefaultAsync(n => n.Id == id);
            _context.Artists.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            var result = await _context.Artists.ToListAsync();
            return result; 
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            var result = await _context.Artists.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<Artist> UpdateAsync(int id, Artist artist)
        {
            _context.Update(artist);
            await _context.SaveChangesAsync();
            return artist;
        }
    }
}
