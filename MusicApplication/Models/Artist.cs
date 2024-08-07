using System.ComponentModel.DataAnnotations;

namespace MusicApplication.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePictureUrl { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        //Relationships
        public List<Artist_Song> Artists_Songs { get; set; }

    }
}
