using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UF3_test.model
{
    [Serializable]
    public class Book
    {
        [JsonProperty("_id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public String Title { get; set; }

        [JsonProperty("isbn")]
        public String Isbn { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount { get; set; }

        [JsonProperty("publishedDate")]
        public PublishedDate PublishedDate { get; set; }

        [JsonProperty("thumbnailUrl")]
        public String ThumbnailUrl { get; set; }

        [JsonProperty("shortDescription")]
        public String ShortDescription { get; set; }

        [JsonProperty("longDescription")]
        public String LongDescription { get; set; }

        [JsonProperty("status")]
        public String Status { get; set; }

        [JsonProperty("authors")]
        public List<String> Authors { get; set; }

        [JsonProperty("categories")]
        public List<String> Categories { get; set; }
    
        public override string ToString()
        {
            return 
                "Book{" + 
                "Id = '" + Id + '\'' +
                ",Title = '" + Title + '\'' + 
                ",Isbn = '" + Isbn + '\'' + 
                ",PageCount = '" + PageCount + '\'' + 
                ",PublishedDate = '" + PublishedDate + '\'' + 
                ",ThumbnailUrl = '" + ThumbnailUrl + '\'' + 
                ",ShortDescription = '" + ShortDescription + '\'' + 
                ",LongDescription = '" + LongDescription + '\'' + 
                ",Status = '" + Status + '\'' + 
                ",Authors = '" + Authors + '\'' + 
                ",Categories = '" + Categories + '\'' + 
                "}";
        }
    }
    
}