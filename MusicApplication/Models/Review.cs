using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApplication.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Comment is required")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string Comment { get; set; }
        public int? Rating { get; set; }

        //Relations
        public int SongId { get; set; }
        [ForeignKey("SongId")]
        public Song ? Song { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "User is required")]
        [StringLength(450, MinimumLength = 1, ErrorMessage = "Invalid Length")]

        public string AppUser { get; set; }
        [ForeignKey("AppUser")]
        public AppUser ? AppUserInfo { get; set; }

    }
}
