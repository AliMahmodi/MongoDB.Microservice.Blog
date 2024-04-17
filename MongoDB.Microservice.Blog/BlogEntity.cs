using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using ThirdParty.Json.LitJson;

namespace MongoDB.Microservice.Blog
{
    public class BlogEntity
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? PublishDateStr { get; set; }
        public string? CreateUserName { get; set; }
        public string? CreateUserImageUrl { get; set; }
    }

    public class BlogDetails
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public IEnumerable<Media>? Medias { get; set; }
        public string? Body { get; set; }
        public int? CreateUserId { get; set; }
        public string? CreateUserName { get; set; }
        public IEnumerable<Media>? CreateUserMedias { get; set; }
        public DateTime? CreateUserLatestActitityDate  { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime? PublishDate { get; set; }
        public bool IsPublished { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public int? DeleteUserId { get; set; }
        public string? DeleteUserName { get; set; }
        public int Version { get; set; } = 0;
        public DateTime? LatestUpdateDate { get; set; } = DateTime.UtcNow;
        public int? UpdateUserId { get; set; }
        public string? UpdateUserName { get; set; }
        public IEnumerable<string>? Tags { get; set; }
        public string? PostTypeStr
        {
            get
            {
                return PostTypeDictionaryClass.PostTypeDictionary?.FirstOrDefault(e => e.Key == PostType).Value;
            }
        }
        public PostType? PostType { get; set; }
    }

    public class Media
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public MediaType MediaType { get; set; }
        public MediaSize MediaSize{ get; set; }
    }

    public enum MediaSize
    {
        Tumbnill,
        Small,
        Medium,
        Large,
        FullSize
    }

    public enum MediaType
    {
        Image,
        Video
    }

    public enum PostType
    {
        Post,
        Guid,
        Poll,
        Location,
        Story,
        Exprience,
        Media

    }

    public class PostTypeDictionaryClass
    {
        public static Dictionary<PostType, string> PostTypeDictionary = new Dictionary<PostType, string>()
        {
            { PostType.Post, "پست" },
            {PostType.Guid, "راهنمایی"},
            {PostType.Poll, "سوال" },
            {PostType.Location, "محل " },
            {PostType.Story, "خاطره" },
            {PostType.Exprience, "تجربه" },
            {PostType.Media, "فیلمو عکس" }
        };
    }
}
