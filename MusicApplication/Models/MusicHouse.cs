using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApplication.Models
{
    public class MusicHouse
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        public string? ProfilePictureUrl { get; set; }
        [NotMapped]
        public IFormFile? CoverUrl { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Bio { get; set; }
        
        //Relationships
        public List<Song>? Songs { get; set; }
    }
}
