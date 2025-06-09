using Newtonsoft.Json;
using System;

namespace UF3_test.model
{
    [Serializable]
    public class Student
    {
        [JsonProperty("_id")]
        public Id Id { get; set; }

        [JsonProperty("firstname")]
        public String Firstname { get; set; }

        [JsonProperty("lastname1")]
        public String Lastname1 { get; set; }

        [JsonProperty("lastname2")]
        public String Lastname2 { get; set; }

        [JsonProperty("dni")]
        public String Dni { get; set; }

        [JsonProperty("gender")]
        public String Gender { get; set; }

        [JsonProperty("email")]
        public String Email { get; set; }

        [JsonProperty("phone")]
        public String Phone { get; set; }

        [JsonProperty("phone_aux")]
        public String Phone_aux { get; set; }

        [JsonProperty("birth_year")]
        public int Birth_year { get; set; }
    }
}