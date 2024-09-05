using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApplication.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cover")]
        public string? Cover { get; set; }
        [NotMapped]
        public IFormFile? CoverUrl { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        //Relationships
        public List<Song> ? Songs { get; set; }
    }
}
