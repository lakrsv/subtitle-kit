namespace SubtitleKitLib.Azure
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using SubtitlesParser.Classes;

    public class TranslationService
    {
        private const int KeyExpirationTimeSeconds = 550;

        private static string _authorizationKey;

        private static DateTime? _keyGenerationTime;

        public static object HttpUtility { get; private set; }

        public static async Task<SubtitleItem[]> TranslateArrayAsync(
            SubtitleItem[] items,
            CultureInfo culture,
            string authToken)
        {
            var to = culture.TwoLetterISOLanguageName;
            var uri = "https://api.microsofttranslator.com/v2/Http.svc/TranslateArray";
            var requestBodies = CreateRequestBodies(items, to);

            var authKey = await GetAuthToken(authToken);

            if (authKey == null) throw new ArgumentNullException(nameof(authKey));

            var sourceTextCounter = 0;

            foreach (var requestBody in requestBodies)
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage())
                    {
                        request.Method = HttpMethod.Post;
                        request.RequestUri = new Uri(uri);
                        request.Content = new StringContent(requestBody, Encoding.UTF8, "text/xml");
                        request.Headers.Add("Authorization", "Bearer " + authKey);
                        var response = await client.SendAsync(request);
                        var responseBody = await response.Content.ReadAsStringAsync();
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                Console.WriteLine("Request status is OK. Result of translate array method is:");
                                var doc = XDocument.Parse(responseBody);
                                var ns = XNamespace.Get(
                                    "http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2");

                                foreach (var xe in doc.Descendants(ns + "TranslateArrayResponse"))
                                {
                                    items[sourceTextCounter].Lines.Clear();

                                    foreach (var node in xe.Elements(ns + "TranslatedText"))
                                        items[sourceTextCounter].Lines.Add(node.Value);

                                    sourceTextCounter++;
                                }

                                break;
                            default:
                                Console.WriteLine("Request status code is: {0}.", response.StatusCode);
                                Console.WriteLine("Request error message: {0}.", responseBody);
                                break;
                        }
                    }
                }

            return items;
        }

        private static List<string> CreateRequestBodies(SubtitleItem[] items, string languageCode)
        {
            var builder = new StringBuilder();
            var itemQueue = new Queue<SubtitleItem>(items);
            var requestBodies = new List<string>();

            while (itemQueue.Count > 0)
            {
                // Header
                builder.Append("<TranslateArrayRequest>");
                builder.Append("<AppId />");
                builder.Append("<From />");
                builder.Append("<Options>");
                builder.Append(
                    "<Category xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />");
                builder.Append(
                    "<ContentType xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />");
                builder.Append(
                    "<ReservedFlags xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />");
                builder.Append(
                    "<State xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" >0</State>");
                builder.Append("<Uri xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />");
                builder.Append(
                    "<User xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />");
                builder.Append("</Options>");
                builder.Append("<Texts>");

                // Content
                for (var i = 0; i < 200; i++)
                {
                    if (itemQueue.Count == 0) break;

                    var nextItem = itemQueue.Dequeue();

                    builder.Append("<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\">");
                    builder.AppendJoin('\n', nextItem.Lines);
                    builder.Append("</string>");
                }

                // Footer
                builder.Append("</Texts>");
                builder.Append("<To>");
                builder.Append(languageCode);
                builder.Append("</To>");
                builder.Append("</TranslateArrayRequest>");

                requestBodies.Add(builder.ToString());
                builder.Clear();
            }

            return requestBodies;
        }

        private static async Task<string> GetAuthToken(string key)
        {
            if (_keyGenerationTime.HasValue)
                if (DateTime.Now.TimeOfDay.TotalSeconds - _keyGenerationTime.Value.TimeOfDay.TotalSeconds
                    < KeyExpirationTimeSeconds) return _authorizationKey;

            _keyGenerationTime = null;

            var uri = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(uri);
                    request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                    var response = await client.SendAsync(request);
                    var responseBody = await response.Content.ReadAsStringAsync();

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            _authorizationKey = responseBody;
                            _keyGenerationTime = DateTime.Now;
                            return _authorizationKey;
                        default:
                            return null;
                    }
                }
            }
        }
    }
}