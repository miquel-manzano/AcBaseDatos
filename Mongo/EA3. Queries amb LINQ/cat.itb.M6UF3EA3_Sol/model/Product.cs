using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6UF3EA3_Sol.model
{
    [Serializable]
    public class Product
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        public String Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public String Picture { get; set; }
        public List<String> Categories { get; set; }
    }
}
