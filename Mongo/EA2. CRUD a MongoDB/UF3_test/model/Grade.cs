﻿using Newtonsoft.Json;
using System;

namespace UF3_test.model
{
    [Serializable]
    public class Grade
{
        //ATTRIBUTES

        [JsonProperty("date")]
        public Date _Date { get; set; }

        [JsonProperty("grade")]
        public String _Grade { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }
    
    //ToSTRING
    public override string ToString()
    {
        return "_Date: " + _Date + "  _Grade: " + _Grade + "   _Score: " + Score;
    }
    
}
}