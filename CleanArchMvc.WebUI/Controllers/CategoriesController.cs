using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories);
        }

        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryDTO category)
        {
            if(ModelState.IsValid)
            {
                _categoryService.Add(category);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var categoryDTO = await _categoryService.GetById(id);

            if(categoryDTO == null) return NotFound();

            return View(categoryDTO);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.Update(categoryDTO);

                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDTO);
        }
    }
}
