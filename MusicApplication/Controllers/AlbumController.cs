using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApplication.Data;
using MusicApplication.Data.Repositories;
using MusicApplication.Models;


namespace MusicApplication.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AlbumController(IAlbumService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _service.GetAllAsync();
            return View(albums);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Album album)
        {
            if (album.CoverUrl == null)
            {
                ModelState.AddModelError("CoverUrl", "The cover image is required.");
            }
            else
            {
                string folder = "Albums/";
                album.Cover = await UploadFile(folder, album.CoverUrl);
            }
            if (!ModelState.IsValid)
            {
                return View(album);
            }
            _service.AddAsync(album);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var albumDetails = await _service.GetByIdAsync(id);

            if (albumDetails == null) return View("NotFound");
            return View(albumDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var albumDetails = await _service.GetByIdAsync(id);
            if (albumDetails == null) return View("NotFound");
            return View(albumDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Album album)
        {
            if (!ModelState.IsValid)
            {
                return View(album);
            }
            if (album.CoverUrl != null)
            {
                string folder = "Albums/";
                album.Cover = await UploadFile(folder, album.CoverUrl);
            }
            await _service.UpdateAsync(id, album);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var albumDetails = await _service.GetByIdAsync(id);
            if (albumDetails == null) return View("NotFound");
            return View(albumDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var albumDetails = await _service.GetByIdAsync(id);
            if (albumDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
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

