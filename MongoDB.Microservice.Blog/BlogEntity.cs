using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using ThirdParty.Json.LitJson;

namespace MongoDB.Microservice.Blog
{
    public class BlogEntity
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        //[JsonIgnore]
        public string? id { get; set; }
        //public int? id { get; set; }
        public string? Title { get; set; }
        //public int? creatorId { get; set; }
        //public string? creatorName { get; set; }
        //public string? creatorImageUrl { get; set; }
        //public DateTime? createDate { get; set; }
        public DateTime? publishDate { get; set; }
  
    }
    public class BlogDetails
    {
        //[BsonId]
        //[BsonElement("_id")]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string? _id { get; set; }
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int? CreateUserId { get; set; }
        public string? CreateUserName { get; set; }
        public string? CreateUserImageUrl { get; set; }
        public DateTime? CreateDate { get; set; }=DateTime.UtcNow;
        public DateTime? PublishDate { get; set; }
        public bool IsPublished { get; set; } = false;
        public int version { get; set; } = 0;
        public DateTime? LatestUpdateDate { get; set; }=DateTime.UtcNow;
    }
}
