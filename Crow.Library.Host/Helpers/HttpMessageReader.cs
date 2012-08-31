using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Crow.Library.Host.Helpers
{
    public static class HttpMessageReader
    {
        public static TResult ReadResponseAs<TResult>(HttpRequestMessage message)
        {
            using (Stream stream = (message as HttpRequestMessage).Content.ReadAsStreamAsync().Result)
            {
                byte[] st = new byte[stream.Length];
                stream.Read(st, 0, st.Length);
                string result = Encoding.ASCII.GetString(st);
                return JsonConvert.DeserializeObject<TResult>(result);
            }
        }

    }
}
