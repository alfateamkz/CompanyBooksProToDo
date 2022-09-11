using System.Net;

namespace CompanyBooksProECom.Utils
{
    public static class UrlChecker
    {
        public static bool RemoteFileExists(string url)
        {
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    return (response.StatusCode == HttpStatusCode.OK);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
