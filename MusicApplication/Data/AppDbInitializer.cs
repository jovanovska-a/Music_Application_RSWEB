using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using MusicApplication.Data.Enums;
using MusicApplication.Models;

namespace MusicApplication.Data
{
    public class AppDbInitializer
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            { 
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                
                context.Database.EnsureCreated();

                //Albums
                if (!context.Albums.Any())
                {
                    context.Albums.AddRange(new List<Album>()
                    {
                        new Album()
                        {
                            Name = "Album 1",
                            Cover = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the first album"
                        }
                    });
                    context.SaveChanges();
                }

                //Artists
                if (!context.Artists.Any())
                {
                    context.Artists.AddRange(new List<Artist>()
                    {
                        new Artist()
                        {
                            FullName = "Artist 1",
                            Bio = "This is the Bio of the first artist",
                            ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-1.jpeg"
                        }
                    });
                    context.SaveChanges();
                }

                //MusicHouses
                if (!context.MusicHouses.Any())
                {
                    context.MusicHouses.AddRange(new List<MusicHouse>()
                    {
                        new MusicHouse()
                        {
                            Name = "Music House 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-1.jpeg"
                        }
                    });
                    context.SaveChanges();
                }

                //Songs
                if (!context.Songs.Any())
                {
                    context.Songs.AddRange(new List<Song>()
                    {
                        new Song()
                        {
                            Name = "Song1",
                            Description = "This is the Song1 description",
                            Price = 39.50,
                            ImageUrl = "http://dotnethow.net/images/movies/movie-3.jpeg",
                            Date = new DateOnly(1959, 08, 20),
                            AlbumId = 1,
                            MusicHouseId = 1,
                            Genre = SongGenre.Pop
                        }
                    });
                    context.SaveChanges();
                }

                //Songs&Artists
                if (!context.Artists_Songs.Any())
                {
                    context.Artists_Songs.AddRange(new List<Artist_Song>()
                    {
                        new Artist_Song()
                        {
                            ArtistId = 1,
                            SongId = 1
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
