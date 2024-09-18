using MusicApplication.Models;

namespace MusicApplication.ViewModels
{
    public class ReviewViewModel
    {
        public int SongId { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
    }
}
