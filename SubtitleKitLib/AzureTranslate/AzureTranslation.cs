using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Globalization;
using System;
using System.Xml.Linq;
using System.Text;

namespace AzureServices
{
    public static class AzureTranslation
    {
        public static object HttpUtility { get; private set; }

        public static async Task<string[]> TranslateArrayAsync(string[] lines, CultureInfo culture, string authToken)
        {
            string[] translatedLines = lines;
            var to = culture.TwoLetterISOLanguageName;
            var uri = "https://api.microsofttranslator.com/v2/Http.svc/TranslateArray";
            var requestBody = CreateRequestBody(lines, to);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "text/xml");
                request.Headers.Add("Authorization", authToken);
                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        Console.WriteLine("Request status is OK. Result of translate array method is:");
                        var doc = XDocument.Parse(responseBody);
                        var ns = XNamespace.Get("http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2");

                        var sourceTextCounter = 0;
                        foreach (XElement xe in doc.Descendants(ns + "TranslateArrayResponse"))
                        {
                            foreach (var node in xe.Elements(ns + "TranslatedText"))
                            {
                                translatedLines[sourceTextCounter] = node.Value;
                            }
                            sourceTextCounter++;
                        }
                        break;
                    default:
                        Console.WriteLine("Request status code is: {0}.", response.StatusCode);
                        Console.WriteLine("Request error message: {0}.", responseBody);
                        break;
                }
            }

            return translatedLines;
        }

        private static string CreateRequestBody(string[] lines, string languageCode)
        {
            var builder = new StringBuilder();

            //Header
            builder.Append("<TranslateArrayRequest>");
            builder.Append("<AppId />");
            builder.Append("<From></From>");
            builder.Append("<Options>");
            builder.Append("<Category xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />");
            builder.Append("<ContentType xmlns =\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\">");
            builder.Append("text/plain");
            builder.Append("</ContentType>");
            builder.Append("<ReservedFlags xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />");
            builder.Append("<State xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />");
            builder.Append("<Uri xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />");
            builder.Append("<User xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />");
            builder.Append("</Options>");
            builder.Append("<Texts>");

            //Content
            foreach (var line in lines)
            {
                builder.Append("<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\">");
                builder.Append(line);
                builder.Append("</string>");
            }

            //Footer
            builder.Append("</Texts>");
            builder.Append("<To>");
            builder.Append(languageCode);
            builder.Append("</To>");
            builder.Append("</TranslateArrayRequest>");

            return builder.ToString();
        }
    }
}
