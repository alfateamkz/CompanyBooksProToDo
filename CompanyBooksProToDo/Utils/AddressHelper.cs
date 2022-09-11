using System;
using System.IO;
using System.Net;
using System.Text;
using CompanyBooksProECom.Models;
using Newtonsoft.Json;
// ReSharper disable PossibleNullReferenceException

namespace CompanyBooksProECom.Utils
{
    public static class AddressHelper
    {
        private const string BaseUrl = "https://nominatim.openstreetmap.org/";
        private const string UserAgentKey = "user-agent";
        private const string UserAgentValue = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";

        public static AddressDescription GetAddress(string lat, string lon)
        {
            var address = new AddressDescription();

            try
            {
                var url = $"{BaseUrl}reverse?format=json&lat={lat}&lon={lon}";
                var request = WebRequest.Create(url) as HttpWebRequest;
                request?.Headers.Add(UserAgentKey, UserAgentValue);

                using var response = request.GetResponse() as HttpWebResponse;
                using var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                address = JsonConvert.DeserializeObject<AddressDescription>(reader.ReadToEnd());

                return address;
            }
            catch (Exception)
            {
                return address;
            }
        }
    }
}
