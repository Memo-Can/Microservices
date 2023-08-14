using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Services;
using Course.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Catalog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : CustomBaseController
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService catogoryService)
		{
			_categoryService = catogoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return CreateActionResultInstance(await _categoryService.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			return CreateActionResultInstance(await _categoryService.GetByIdAsync(id));
		}

		[HttpPost]
		public async Task<IActionResult> Create(CategoryDto categoryDto)
		{
			return CreateActionResultInstance(await _categoryService.CreateAsync(categoryDto));
		}
	}
}