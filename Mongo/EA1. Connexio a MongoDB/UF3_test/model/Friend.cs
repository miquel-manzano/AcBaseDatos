﻿using Newtonsoft.Json;
using System;

namespace UF3_test.model

{
    [Serializable]
    public class Friend
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

