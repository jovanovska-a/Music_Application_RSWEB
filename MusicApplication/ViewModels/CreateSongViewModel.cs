using MusicApplication.Data.Enums;
using MusicApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApplication.ViewModels
{
    public class CreateSongViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Download { get; set; }
        public double Price { get; set; }
        public string ? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public DateOnly Date { get; set; }
        public SongGenre Genre { get; set; }
        //Relations
        [Required(ErrorMessage = "Album is required")]
        public int AlbumId { get; set; }
        public IEnumerable<Album>? Album { get; set; }

        [Required(ErrorMessage = "Music house is required")]
        public int MusicHouseId { get; set; }
        public IEnumerable<MusicHouse>? MusicHouse { get; set; }

        [Required(ErrorMessage = "Artist is required")]
        public IEnumerable<int> ArtistIds { get; set; }
        public IEnumerable<Artist>? Artists { get; set; }
    }
}
