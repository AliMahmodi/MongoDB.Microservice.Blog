using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using ThirdParty.Json.LitJson;

namespace MongoDB.Microservice.Blog
{
    public class BlogModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? ImagrUrl { get; set; }
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
        public string? CreateUserLocation { get; set; }
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
        public string? PostTypeStr => PostTypeDictionaryClass.PostTypeDictionary?.FirstOrDefault(e => e.Key == PostType).Value;

        public PostType? PostType { get; set; }
        public int TotalLikesCount { get; set; } = 0;
        public int TotalCommentsCount { get; set; } = 0;
        public bool? IsMarkedAsSaved { get; set; } = false;
        public bool? IsMarkedAsRead { get; set; } = false;

        public bool? IsApprovedByAdmin { get; set; } = false;
        public DateTime? AdminApproveDate { get; set; }
        public int? AdminApproverId { get; set; }
        public string? AdminApproverName { get; set; }

    }

    public class Media
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Alt { get; set; }
        public string? Description { get; set; }
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
        Video,
        Voice,
        Map,
        File,
        Link,
        JSON,
    }

    public enum PostType
    {
        Post,
        Guide,
        Poll,
        Location,
        Story,
        Exprience,
        Media

    }

    public class PostTypeDictionaryClass
    {
        public static Dictionary<PostType, string> PostTypeDictionary = new()
        {
            {PostType.Post, "پست" },
            {PostType.Guide, "راهنمایی"},
            {PostType.Poll, "سوال" },
            {PostType.Location, "محل " },
            {PostType.Story, "خاطره" },
            {PostType.Exprience, "تجربه" },
            {PostType.Media, "فیلمو عکس" }
        };
    }
}
