using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using Course.Shared.Dtos;
using Microsoft.AspNetCore.Authentication;
using MongoDB.Driver;

namespace Course.Services.Catalog.Services
{
	public class CourseService : ICourseService
	{
		private readonly IMongoCollection<CourseModel> _courseCollection;
		private readonly IMongoCollection<CategoryModel> _categoryCollection;
		private readonly IMapper _mapper;

		public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
		{
			var client = new MongoClient(databaseSettings.ConnectionString);
			var database = client.GetDatabase(databaseSettings.DatabaseName);

			_courseCollection = database.GetCollection<CourseModel>(databaseSettings.CourseCollectionName);
			_categoryCollection = database.GetCollection<CategoryModel>(databaseSettings.CategoryCollectionName);

			_mapper = mapper;
		}

		public async Task<Response<List<CourseDto>>> GetAllAsync()
		{
			var courses = await _courseCollection.Find(x => true).ToListAsync();

			if (courses.Any())
				foreach (var course in courses)
					course.Category = await _categoryCollection.Find<CategoryModel>(x => x.Id == course.CategoryId).FirstAsync();
			else
				courses = new List<CourseModel>();

			return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
		}

		public async Task<Response<CourseDto>> GetByIdAsync(string id)
		{
			var course = await _courseCollection.Find<CourseModel>(x => x.Id == id).FirstOrDefaultAsync();

			if (course == null)
				return Response<CourseDto>.Fail("Course Not found", 404);

			course.Category = await _categoryCollection.Find<CategoryModel>(x => x.Id == course.CategoryId).FirstAsync();

			return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
		}

		public async Task<Response<List<CourseDto>>> GetAddByUserIdAsync(string userId)
		{
			var courses = await _courseCollection.Find<CourseModel>(x => x.UserId == userId).ToListAsync();

			if (courses.Any())
				foreach (var course in courses)
					course.Category = await _categoryCollection.Find<CategoryModel>(x => x.Id == course.CategoryId).FirstAsync();
			else
				courses = new List<CourseModel>();

			return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);

		}

		public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
		{
			var course = _mapper.Map<CourseModel>(courseCreateDto);

			course.CreateDate = DateTime.Now;
			await _courseCollection.InsertOneAsync(course);

			return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
		}

		public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
		{
			var course = _mapper.Map<CourseModel>(courseUpdateDto);
			var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, course);

			if (result == null)
				return Response<NoContent>.Fail("Course Not Found", 404);

			return Response<NoContent>.Success(204);
		}

		public async Task<Response<NoContent>> DeleteAsync(string id)
		{
			var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

			if (result.DeletedCount > 0)
				return Response<NoContent>.Success(204);
			else
				return Response<NoContent>.Fail("Course Not Found", 404);

		}
	}
}
