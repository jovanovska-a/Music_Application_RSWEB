using System.ComponentModel.DataAnnotations;

namespace MusicApplication.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string Cover { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relationships
        public List<Song> Songs { get; set; }
    }
}
