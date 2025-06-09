using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF3_test.model
{
    [Serializable]
    public class Student2
    {

        [JsonProperty("_id")]
        public Id Id { get; set; }

        [JsonProperty("student_id")]
        public  NumberInt Student_id { get; set; }

        [JsonProperty("scores")]
        public List<Score> Scores { get; set; }

        [JsonProperty("class_id")]
        public NumberInt Class_id { get; set; }
        
    }
}
