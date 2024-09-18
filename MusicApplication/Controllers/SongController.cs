using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApplication.Data;
using MusicApplication.Data.Repositories;
using MusicApplication.Data.Services;
using MusicApplication.Models;
using MusicApplication.ViewModels;

namespace MusicApplication.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService _service;
        private readonly IArtistService _artistService;
        private readonly IArtistSongService _artistSongService;
        private readonly IAlbumService _albumService;
        private readonly IMusicHouseService _musicHouseService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SongController(ISongService service, IArtistService artistService,IArtistSongService artistSongService, IAlbumService albumService, IMusicHouseService musicHouseService, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _artistService = artistService;
            _artistSongService = artistSongService;
            _albumService = albumService;
            _musicHouseService = musicHouseService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var songs = await _service.GetAllAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                songs = songs.Where(n => n.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View(songs);
        }
        public async Task<IActionResult> ArtistSongs(int id)
        { 
            var songIds = await _artistSongService.GetSongByArtistIdAsync(id);

            var songs = await _service.GetAllAsync();
            var filteredSongs = songs.Where(s => songIds.Contains(s.Id)).ToList();
            return View(filteredSongs);
        }
        public async Task<IActionResult> AlbumSongs(int id)
        {
            var songIds = await _service.GetSongByAlbumIdAsync(id);
            var songs = await _service.GetAllAsync();
            var filteredSongs = songs.Where(s => songIds.Contains(s.Id)).ToList();
            return View(filteredSongs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var songDetails = await _service.GetByIdAsync(id);

            if (songDetails == null) return View("NotFound");
            return View(songDetails);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<Album> albums = await _albumService.GetAllAsync();
            IEnumerable<MusicHouse> musicHouses = await _musicHouseService.GetAllAsync();
            IEnumerable<Artist> artists = await _artistService.GetAllAsync();
            CreateSongViewModel newVM = new CreateSongViewModel()
            {
               Album = albums,
               MusicHouse = musicHouses,
               Artists = artists
            };
            return View(newVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSongViewModel songVM)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Album> albums = await _albumService.GetAllAsync();
                IEnumerable<MusicHouse> musicHouses = await _musicHouseService.GetAllAsync();
                IEnumerable<Artist> artists = await _artistService.GetAllAsync();
                songVM.Album = albums;
                songVM.MusicHouse = musicHouses;
                songVM.Artists = artists;
                ModelState.AddModelError("", "Failed to add song");
                return View(songVM);
            }
            var newSong = new Song()
            {
                Name = songVM.Name,
                Genre = songVM.Genre,
                Description = songVM.Description,
                Download = songVM.Download,
                Date = songVM.Date,
                AlbumId = songVM.AlbumId,
                MusicHouseId = songVM.MusicHouseId
            };
            if(songVM.Image != null)
            {
                string folder = "Songs/";
                newSong.ImageUrl = await UploadFile(folder, songVM.Image);
            }
            _service.AddAsync(newSong);

            Song newLastSong = await _service.GetLastSong();
            foreach (var artistId in songVM.ArtistIds)
            {
                Artist_Song artistSong = new Artist_Song()
                {
                    SongId = newLastSong.Id,
                    ArtistId = artistId,
                };
                _artistSongService.Add(artistSong);
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int id)
        {
            Song song = await _service.GetByIdAsyncNoTracking(id);
            if (song == null)
            {
                return View("NotFound");
            }
            IEnumerable<Album> album = await _albumService.GetAllAsync();
            IEnumerable<MusicHouse> musicHouse = await _musicHouseService.GetAllAsync();
            IEnumerable<Artist> artists = await _artistService.GetAllAsync();
            List<int> artistIds = new List<int>();
            foreach (var artist in song.Artists_Songs)
            {
                artistIds.Add(artist.ArtistId);
            }
            EditSongViewModel songVM = new EditSongViewModel()
            {
                Id = song.Id,
                Name = song.Name,
                Date = song.Date,
                ImageUrl = song.ImageUrl,
                Description = song.Description,
                Genre = song.Genre,
                AlbumId = song.AlbumId,
                MusicHouseId = song.MusicHouseId,
                ArtistIds = artistIds,
                Artists = artists,
                Album = album,
                MusicHouse = musicHouse,
            };
            return View(songVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSongViewModel songVM)
        {
            if (!ModelState.IsValid)
            {
                songVM.Album = await _albumService.GetAllAsync();
                songVM.MusicHouse = await _musicHouseService.GetAllAsync();
                songVM.Artists = await _artistService.GetAllAsync();
                ModelState.AddModelError("", "Failed to edit book");
                return View(songVM);
            }
            if (songVM.Image != null)
            {
                string folder = "Songs/";
                songVM.ImageUrl = await UploadFile(folder, songVM.Image);
            }
            if (songVM != null)
            {
                Song newSong = new Song()
                {
                    Id = songVM.Id,
                    Name = songVM.Name,
                    Genre = songVM.Genre,
                    Description = songVM.Description,
                    Date = songVM.Date,
                    AlbumId = songVM.AlbumId,
                    MusicHouseId = songVM.MusicHouseId,
                    ImageUrl = songVM.ImageUrl
                };
                await _service.UpdateAsync(id, newSong);
                IEnumerable<Artist_Song> artistSongs = await _artistSongService.GetAllAsync();
                foreach (var sa in artistSongs)
                {
                    if (sa.SongId == id)
                    {
                        _artistSongService.Delete(sa);
                    }
                }
                foreach (var artistId in songVM.ArtistIds)
                {
                    Artist_Song artistSong = new Artist_Song()
                    {
                        SongId = songVM.Id,
                        ArtistId = artistId,
                    };
                    _artistSongService.Add(artistSong);
                }

                return Redirect("/Song/Details/" + id);
            }
            else
            {
                songVM.Album = await _albumService.GetAllAsync();
                songVM.MusicHouse = await _musicHouseService.GetAllAsync();
                songVM.Artists = await _artistService.GetAllAsync();
                ModelState.AddModelError("", "Failed to edit book");
                return View(songVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Song song = await _service.GetByIdAsync(id);
            if (song == null)
            {
                return View("NotFound");
            }
            return View(song);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSongConfirmed(int id)
        {
            Song song = await _service.GetByIdAsync(id);
            if (song == null)
            {
                return View("NotFound");
            }
            await _artistSongService.DeleteBySongIdAsync(id);
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            if (folderPath == null)
            {
                throw new ArgumentNullException(nameof(folderPath), "Folder path cannot be null.");
            }
            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(folderPath, fileName);
            string absoluteServerPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
            using (var stream = new FileStream(absoluteServerPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/" + filePath.Replace("\\", "/");
        }

    }
}
