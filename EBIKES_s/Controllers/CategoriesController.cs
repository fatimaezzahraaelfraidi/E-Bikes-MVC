using EBIKES_s.Data;
using EBIKES_s.Data.Services;
using EBIKES_s.Data.Static;
using EBIKES_s.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBIKES_s.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _service;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ICategoriesService service, ILogger<CategoriesController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCategories = await _service.GetAllAsync();
            return View(allCategories);
        }
        //Get: categorys/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ImageUrl,Title,Description")] Category category)
        {
            if (!ModelState.IsValid) return View(category);
            await _service.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        //Get: Categories/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        //Get: Categories/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageUrl,Title,Description")] Category category)
        {
            if (!ModelState.IsValid) return View(category);
            await _service.UpdateAsync(id, category);
            return RedirectToAction(nameof(Index));
        }

        //Get: categorys/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
