using System.ComponentModel.DataAnnotations;

namespace MusicApplication.Models
{
    public class MusicHouse
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePictureUrl { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }
        
        //Relationships
        public List<Song> Songs { get; set; }
    }
}
