using Microsoft.EntityFrameworkCore;
using MusicApplication.Data.Repositories;
using MusicApplication.Models;

namespace MusicApplication.Data.Services
{
    public class ArtistSongService : IArtistSongService
    {
        private readonly AppDbContext _context;

        public ArtistSongService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Artist_Song artistSong)
        {
            _context.Artists_Songs.Add(artistSong);
            _context.SaveChanges();
        }

        public bool Delete(Artist_Song artistSong)
        {
            _context.Artists_Songs.Remove(artistSong);
            return Save();
        }

        public async Task<IEnumerable<Artist_Song>> GetAllAsync()
        {
            return await _context.Artists_Songs.AsNoTracking().ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<Artist_Song> UpdateAsync(int id, Artist_Song artistSong)
        {
            _context.Update(artistSong);
            await _context.SaveChangesAsync();
            return artistSong;
        }

        public async Task DeleteBySongIdAsync(int songId)
        {
            var artistSongs = _context.Artists_Songs.Where(sa => sa.SongId == songId);
            _context.Artists_Songs.RemoveRange(artistSongs);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<int>> GetSongByArtistIdAsync(int artistId)
        {
            var artistSongs = await _context.Artists_Songs.Where(sa => sa.ArtistId == artistId).Select(sa => sa.SongId).ToListAsync();
            return artistSongs;
        }
    }
}
