using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApplication.Data;
using MusicApplication.Data.Repositories;
using MusicApplication.Models;

namespace MusicApplication.Controllers
{
    public class MusicHouseController : Controller
    {
        private readonly IMusicHouseService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MusicHouseController(IMusicHouseService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var music_houses = await _service.GetAllAsync();
            return View(music_houses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var musicHouseDetails = await _service.GetByIdAsync(id);

            if (musicHouseDetails == null) return View("NotFound");
            return View(musicHouseDetails);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MusicHouse music_house)
        {
            if (music_house.CoverUrl == null)
            {
                ModelState.AddModelError("CoverUrl", "The cover image is required.");
            }
            else
            {
                string folder = "MusicHouses/";
                music_house.ProfilePictureUrl = await UploadFile(folder, music_house.CoverUrl);
            }
            if (!ModelState.IsValid)
            {
                return View(music_house);
            }
            _service.AddAsync(music_house);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var musicHouseDetails = await _service.GetByIdAsync(id);
            if (musicHouseDetails == null) return View("NotFound");
            return View(musicHouseDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MusicHouse music_house)
        {
            if (!ModelState.IsValid)
            {
                return View(music_house);
            }
            if (music_house.CoverUrl != null)
            {
                string folder = "MusicHouses/";
                music_house.ProfilePictureUrl = await UploadFile(folder, music_house.CoverUrl);
            }
            await _service.UpdateAsync(id, music_house);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var musicHouseDetails = await _service.GetByIdAsync(id);
            if (musicHouseDetails == null) return View("NotFound");
            return View(musicHouseDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicHouseDetails = await _service.GetByIdAsync(id);
            if (musicHouseDetails == null) return View("NotFound");
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
