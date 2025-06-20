﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF3_test.model
{
    public class Language
    {
        [JsonProperty("iso639_1")]
        public String Iso639_1 { get; set; }

        [JsonProperty("iso639_2")]
        public String Iso639_2 { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("nativeName")]
        public String NativeName { get; set; }
    }
}
