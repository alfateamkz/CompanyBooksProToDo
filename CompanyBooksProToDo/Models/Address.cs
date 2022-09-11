using Newtonsoft.Json;

namespace CompanyBooksProECom.Models
{
    public class AddressDescription
    {
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }
    }

    public class Address
    {
        [JsonProperty(PropertyName = "amenity")]
        public string Amenity { get; set; }
        [JsonProperty(PropertyName = "road")]
        public string Road { get; set; }
        [JsonProperty(PropertyName = "city_district")]
        public string CityDistrict { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }
        [JsonProperty(PropertyName = "postcode")]
        public string PostCode { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }
    }
}
