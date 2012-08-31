using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;

namespace Crow.Library.Tests.Hosting
{
    class ClientHelper
    {
        internal static object Call(string hostname, int port, string url, object parameters = null)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://{0}:{1}".FormatText(hostname, port))
            };
            if (parameters == null)
            {
                return client.GetStringAsync(url).Result;
            }
            else
            {
                string contentText = JsonConvert.SerializeObject(parameters);
                HttpContent content = new StringContent(contentText);
                return client.PostAsync(url, content).Result;
            }
        }

        internal static string ReadFromStreamToString(HttpResponseMessage httpResponseMessage)
        {
            Stream stream = (httpResponseMessage as HttpResponseMessage).Content.ReadAsStreamAsync().Result;
            byte[] st = new byte[stream.Length];
            stream.Read(st, 0, st.Length);
            return Encoding.ASCII.GetString(st);
        }
    }
}
