﻿using System.Text.Json.Serialization;

namespace WebApplicationRedir.Models
{
    public class PhotoModel
    {
        [JsonPropertyName("albumId")]
        public int AlbumId { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }
}
