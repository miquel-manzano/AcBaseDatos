using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA3_Sol.model
{
  
    [Serializable]
    public class Book
    {
        [JsonProperty("_id")]
        public int Id { get; set; }
        public String Title { get; set; }
        public String Isbn { get; set; }
        public int PageCount { get; set; }
        public PublishedDate PublishedDate { get; set; }
        public String ThumbnailUrl { get; set; }
        public String ShortDescription { get; set; }
        public String LongDescription { get; set; }
        public String Status { get; set; }
        public List<String> Authors { get; set; }
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
