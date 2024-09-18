using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MusicApplication.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<User_Song>? User_Songs { get; set; }
    }
}
