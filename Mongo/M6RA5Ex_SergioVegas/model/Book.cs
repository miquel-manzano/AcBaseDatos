using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace M6RA5Ex.model
{
    [Serializable]
    public class Book
    {

        [JsonProperty("_id")]
        [BsonElement("_id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        [BsonElement("title")]
        public String Title { get; set; }

        [JsonProperty("isbn")]
        [BsonElement("isbn")]
        public String Isbn { get; set; }

        [JsonProperty("pageCount")]
        [BsonElement("pageCount")]
        public int PageCount { get; set; }

        [JsonProperty("publishedDate")]
        [BsonElement("publishedDate")]
        public PublishedDate PublishedDate { get; set; }

        [JsonProperty("thumbnailUrl")]
        [BsonElement("thumbnailUrl")]
        public String ThumbnailUrl { get; set; }

        [JsonProperty("shortDescription")]
        [BsonElement("shortDescription")]
        public String ShortDescription { get; set; }

        [JsonProperty("longDescription")]
        [BsonElement("longDescription")]
        public String LongDescription { get; set; }

        [JsonProperty("status")]
        [BsonElement("status")]
        public String Status { get; set; }

        [JsonProperty("authors")]
        [BsonElement("authors")]
        public List<String> Authors { get; set; }

        [JsonProperty("categories")]
        [BsonElement("categories")]
        public List<String> Categories { get; set; }

    }
}
