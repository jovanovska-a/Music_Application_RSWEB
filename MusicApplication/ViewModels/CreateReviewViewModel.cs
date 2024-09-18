using MusicApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicApplication.ViewModels
{
    public class CreateReviewViewModel
    {
        [Display(Name = "User")]
        public string UserName { get; set; }
        public Review Review { get; set; }
    }
}
