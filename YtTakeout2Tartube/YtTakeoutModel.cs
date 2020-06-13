using System;
using Newtonsoft.Json;

namespace YtTakeout2Tartube
{
    public class YtTakeoutModel
    {
        public class Subscription
        {
            [JsonProperty("contentDetails")]
            public ContentDetailsModel ContentDetails { get; set; }
            [JsonProperty("etag")]
            public string Etag { get; set; }
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("kind")]
            public string Kind { get; set; }
            [JsonProperty("snippet")]
            public SnippetModel Snippet { get; set; }
        }

        public class ContentDetailsModel
        {
            [JsonProperty("activityType")]
            public string ActivityType { get; set; }
            [JsonProperty("newItemCount")]
            public int NewItemCount { get; set; }
            [JsonProperty("totalItemCount")]
            public int TotalItemCount { get; set; }
        }

        public class SnippetModel
        {
            [JsonProperty("channelId")]
            public string ChannelId { get; set; }
            [JsonProperty("description")]
            public string Description { get; set; }
            [JsonProperty("publishedAt")]
            public DateTime PublishedAt { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("resourceId")]
            public ResourceIdModel ResourceId { get; set; }
        }

        public class ResourceIdModel
        {
            [JsonProperty("channelId")]
            public string ChannelId { get; set; }
            [JsonProperty("kind")]
            public string Kind { get; set; }
        }
    }
}