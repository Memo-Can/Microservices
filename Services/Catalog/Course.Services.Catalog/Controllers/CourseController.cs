using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Services;
using Course.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Catalog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseController : CustomBaseController
	{
		private readonly ICourseService _courseService;

		public CourseController(ICourseService courseService)
		{
			_courseService = courseService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _courseService.GetAllAsync();

			return CreateActionResultInstance(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			return CreateActionResultInstance(await _courseService.GetByIdAsync(id));
		}

		[HttpGet]
		//[HttpGet("user/{userId}")]
		[Route("/api/[controller]/GetAllByUserId/{userId}")]
		public async Task<IActionResult> GetAllByUserId(string userId)
		{
			return CreateActionResultInstance(await _courseService.GetAddByUserIdAsync(userId));
		}

		[HttpPost]
		public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
		{
			return CreateActionResultInstance(await _courseService.CreateAsync(courseCreateDto));
		}

		[HttpPut]
		public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
		{
			return CreateActionResultInstance(await _courseService.UpdateAsync(courseUpdateDto));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			return CreateActionResultInstance(await _courseService.DeleteAsync(id));
		}
	}
}