using Course.Services.Catalog.Dtos;
using Course.Shared.Dtos;

namespace Course.Services.Catalog.Services
{
	public interface ICategoryService
	{
		Task<Response<List<CategoryDto>>> GetAllAsync();
		Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
		Task<Response<CategoryDto>> GetByIdAsync(string id);

	}
}
