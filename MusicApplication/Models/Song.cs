using MusicApplication.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApplication.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateOnly Date { get; set; }
        public SongGenre Genre { get; set; }

        //Relationships
        public List<Artist_Song> Artists_Songs { get; set; }

        //Album
        public int AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        public Album Album { get; set; }

        //Music_House
        public int MusicHouseId { get; set; }
        [ForeignKey("MusicHouseId")]
        public MusicHouse MusicHouse { get; set; }
        
    }
}
