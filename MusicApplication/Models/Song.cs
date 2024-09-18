using MusicApplication.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApplication.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Download Link")]
        public string ? Download { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        [Display(Name = "Date")]
        public DateOnly Date { get; set; }
        [Display(Name = "Genre")]
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

        //Reviews
        public ICollection<Review>? Reviews { get; set; }

        //UserSongs
        public ICollection<User_Song>? UserSongs { get; set; }

    }
}
