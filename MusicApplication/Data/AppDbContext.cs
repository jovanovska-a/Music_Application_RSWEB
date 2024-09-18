using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicApplication.Models;

namespace MusicApplication.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist_Song>().HasKey(sa  => new
            {
                sa.ArtistId,
                sa.SongId
            });

            modelBuilder.Entity<Artist_Song>().HasOne(s => s.Song).WithMany(sa => sa.Artists_Songs).HasForeignKey(s => s.SongId);
            modelBuilder.Entity<Artist_Song>().HasOne(a => a.Artist).WithMany(sa => sa.Artists_Songs).HasForeignKey(a => a.ArtistId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist_Song> Artists_Songs { get; set; }
        public DbSet<MusicHouse> MusicHouses { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User_Song> User_Song { get; set; }
    }
}
