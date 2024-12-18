﻿using Course.Services.Catalog.Dtos;
using Course.Shared.Dtos;

namespace Course.Services.Catalog.Services
{
	public interface ICourseService
	{
		Task<Response<List<CourseDto>>> GetAllAsync();
		Task<Response<CourseDto>> GetByIdAsync(string id);
		Task<Response<List<CourseDto>>> GetAddByUserIdAsync(string userId);
		Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
		Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
		Task<Response<NoContent>> DeleteAsync(string id);
	}
}
