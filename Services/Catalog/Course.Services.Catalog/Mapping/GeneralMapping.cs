using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;

namespace Course.Services.Catalog.Mapping
{
	public class GeneralMapping : Profile
	{
		public GeneralMapping()
		{
			CreateMap<CourseModel, CourseDto>().ReverseMap();
			CreateMap<CategoryModel, CategoryDto>().ReverseMap();
			CreateMap<FeatureModel, FeatureDto>().ReverseMap();

			CreateMap<CourseModel, CourseCreateDto>().ReverseMap();
			CreateMap<CourseModel, CourseUpdateDto>().ReverseMap();
		}
	}
}
