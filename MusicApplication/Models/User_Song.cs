using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApplication.Models
{
    public class User_Song
    {
        [Key]
        public int Id { get; set; }
        public string AppUser { get; set; }
        [ForeignKey("AppUser")]
        public AppUser? AppUserInfo { get; set; }

        public int SongId { get; set; }
        [ForeignKey("SongId")]
        public Song Song { get; set; }
    }
}
