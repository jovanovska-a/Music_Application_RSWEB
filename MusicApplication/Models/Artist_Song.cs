using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApplication.Models
{
    public class Artist_Song
    {
        public int SongId { get; set; }
        public Song Song { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
