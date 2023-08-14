using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Course.Services.Catalog.Models
{
	public class CourseModel
	{
		[BsonId]//Mongo Db primary id olarak algılanmas için
		[BsonRepresentation(BsonType.ObjectId)]//Tip tanımı
		public string Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		[BsonRepresentation(BsonType.Decimal128)]
		public decimal Price { get; set; }

		public string UserId { get; set; }

		public string Picture { get; set; }

		[BsonRepresentation(BsonType.DateTime)]
		public DateTime CreateDate { get; set; }

		public FeatureModel Feature { get; set; } //Nesnenin kendisi 

		[BsonRepresentation(BsonType.ObjectId)]
		public string CategoryId { get; set; }

		[BsonIgnore] //nesne kullanım amacı taşımakta db ye kaydı olmaması için bsonignore eklendi. Serilize edilmesi engellenmiş oldu
		public CategoryModel Category { get; set; }
	}
}
