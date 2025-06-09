using System;
using Newtonsoft.Json;

namespace UF3_test.model
{
    
[Serializable]
public class Address
{
        //ATTRIBUTES

        [JsonProperty("building")]
        public String Building { get; set; }

        [JsonProperty("coord")]
        public double[] Coord { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("zipcode")]
        public String Zipcode { get; set; }


        //ToSTRING
    public override string ToString()
    {
        return "Building: " + Building + "  Coord: " + Coord + "   Street: " + Street + "   Zipcode: " + Zipcode;
    }

}
}