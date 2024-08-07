using System.ComponentModel.DataAnnotations;

namespace MusicApplication.Models
{
    public class MusicHouse
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        
        //Relationships
        public List<Song> Songs { get; set; }
    }
}
