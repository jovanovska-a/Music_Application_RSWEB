using MusicApplication.Data.Enums;
using MusicApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MusicApplication.ViewModels
{
    public class EditSongViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public DateOnly Date { get; set; }
        public SongGenre Genre { get; set; }

        public int AlbumId { get; set; }
        public IEnumerable<Album>? Album { get; set; }

        public int MusicHouseId { get; set; }
        public IEnumerable<MusicHouse>? MusicHouse { get; set; }

        public IEnumerable<int> ArtistIds { get; set; }
        public IEnumerable<Artist>? Artists { get; set; }
    }
}

