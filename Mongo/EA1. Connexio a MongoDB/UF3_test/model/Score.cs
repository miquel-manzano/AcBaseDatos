using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF3_test.model
{

    [Serializable]
    public class Score
    {
        //ATTRIBUTES

        [JsonProperty("type")]
        public String Type { get; set; }

        [JsonProperty("score")]
        public NumberDouble _Score { get; set; }

    }
    
}
