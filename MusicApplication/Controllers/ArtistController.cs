using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApplication.Data;
using MusicApplication.Data.Repositories;
using MusicApplication.Data.Services;
using MusicApplication.Models;

namespace MusicApplication.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArtistController(IArtistService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index(string searchString)
        {
            var artists = await _service.GetAllAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                artists = artists.Where(n => n.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View(artists);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Artist artist)
        {
            if (artist.CoverUrl == null)
            {
                ModelState.AddModelError("CoverUrl", "The cover image is required.");
            }
            else
            {
                string folder = "Artists/";
                artist.ProfilePictureUrl = await UploadFile(folder, artist.CoverUrl);
            }
            if (!ModelState.IsValid)
            { 
                return View(artist);
            }
            _service.AddAsync(artist);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var artistDetails = await _service.GetByIdAsync(id);

            if (artistDetails == null) return View("NotFound");
            return View(artistDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var artistDetails = await _service.GetByIdAsync(id);
            if (artistDetails == null) return View("NotFound");
            return View(artistDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return View(artist);
            }
            if (artist.CoverUrl != null)
            {
                string folder = "Artists/";
                artist.ProfilePictureUrl = await UploadFile(folder, artist.CoverUrl);
            }
            await _service.UpdateAsync(id, artist);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var artistDetails = await _service.GetByIdAsync(id);
            if (artistDetails == null) return View("NotFound");
            return View(artistDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artistDetails = await _service.GetByIdAsync(id);
            if (artistDetails == null) return View("NotFound");
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
