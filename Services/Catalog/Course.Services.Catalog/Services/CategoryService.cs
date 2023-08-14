﻿using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using Course.Shared.Dtos;
using MongoDB.Driver;

namespace Course.Services.Catalog.Services
{
	internal class CategoryService : ICategoryService
	{
		private readonly IMongoCollection<CategoryModel> _categoryCollection;
		private readonly IMapper _mapper;

		public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
		{
			var client = new MongoClient(databaseSettings.ConnectionString);
			var database = client.GetDatabase(databaseSettings.DatabaseName);

			_categoryCollection = database.GetCollection<CategoryModel>(databaseSettings.CategoryCollectionName);
			_mapper = mapper;
		}

		public async Task<Response<List<CategoryDto>>> GetAllAsync()
		{
			var categories = await _categoryCollection.Find(x => true).ToListAsync();

			return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
		}

		public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
		{
			await _categoryCollection.InsertOneAsync(_mapper.Map<CategoryModel>(categoryDto));

			return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(categoryDto), 200);
		}

		public async Task<Response<CategoryDto>> GetByIdAsync(string id)
		{
			var category = await _categoryCollection.Find<CategoryModel>(x => x.Id == id).FirstOrDefaultAsync();

			if (category == null)
				return Response<CategoryDto>.Fail("Category Not Found", 404);

			return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
		}
	}
}
