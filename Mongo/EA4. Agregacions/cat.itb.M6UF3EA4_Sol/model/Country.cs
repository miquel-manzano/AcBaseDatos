using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace cat.itb.M6UF3EA4_Sol.model
{
    [Serializable]
    public class Country
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }

        [JsonProperty("topLevelDomain")]
        [BsonElement("topLevelDomain")]
        public List<string> TopLevelDomain { get; set; }

        [JsonProperty("alpha2Code")]
        [BsonElement("alpha2Code")]
        public string Alpha2Code { get; set; }

        [BsonElement("alpha3Code")]
        [JsonProperty("alpha3Code")]
        public string Alpha3Code { get; set; }

        [JsonProperty("callingCodes")]
        [BsonElement("callingCodes")]
        public List<string> CallingCodes { get; set; }

        [JsonProperty("capital")]
        [BsonElement("capital")]
        public string Capital { get; set; }

        [JsonProperty("altSpellings")]
        [BsonElement("altSpellings")]
        public List<string> AltSpellings { get; set; }

        [JsonProperty("region")]
        [BsonElement("region")]
        public string Region { get; set; }

        [JsonProperty("subregion")]
        [BsonElement("subregion")]
        public string Subregion { get; set; }

        [BsonElement("population")]
        [JsonProperty("population")]
        public int Population { get; set; }

        [JsonProperty("latlng")]
        [BsonElement("latlng")]
        public List<double> Latlng { get; set; }

        [JsonProperty("demonym")]
        [BsonElement("demonym")]
        public string Demonym { get; set; }

        [JsonProperty("area")]
        [BsonElement("area")]
        public double? Area { get; set; }

        [JsonProperty("gini")]
        [BsonElement("gini")]
        public double? Gini { get; set; }

        [JsonProperty("timezones")]
        [BsonElement("timezones")]
        public List<string> Timezones { get; set; }

        [JsonProperty("borders")]
        [BsonElement("borders")]
        public List<string> Borders { get; set; }

        [JsonProperty("nativeName")]
        [BsonElement("nativeName")]
        public string NativeName { get; set; }

        [JsonProperty("numericCode")]
        [BsonElement("numericCode")]
        public string NumericCode { get; set; }

        [JsonProperty("currencies")]
        [BsonElement("currencies")]
        public List<Currency> Currencies { get; set; }

        [JsonProperty("languages")]
        [BsonElement("languages")]
        public List<Language> Languages { get; set; }

        [JsonProperty("translations")]
        [BsonElement("translations")]
        public Translation Translations { get; set; }

        [JsonProperty("flag")]
        [BsonElement("flag")]
        public string Flag { get; set; }

        [JsonProperty("regionalBlocs")]
        [BsonElement("regionalBlocs")]
        public List<RegionalBloc> RegionalBlocs { get; set; }

        [JsonProperty("cioc")]
        [BsonElement("cioc")]
        public string Cioc { get; set; }
    }
}
