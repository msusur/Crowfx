using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Exceptions;

namespace Crow.Library.Foundation.Common.Helpers
{
    /// <summary>
    /// Parses the given http url.
    /// </summary>
    public class UrlParser
    {
        private readonly string _url;
        private readonly static string https = "https://";
        private readonly static string http = "http://";

        public string Hostname { get; set; }
        public bool IsHttps { get; set; }
        public int Port { get; set; }
        public string BusinessClass { get; set; }
        public string BusinessAction { get; set; }
        public Dictionary<string, string> Parameters { get; private set; }

        public UrlParser(string url)
        {
            _url = url;
            Parameters = new Dictionary<string, string>();
            Parse(_url);
        }

        private void Parse(string url)
        {
            IsHttps = url.StartsWith(https);
            url = url.Replace(IsHttps ? https : http, string.Empty);
            string[] splits = url.Split('/');
            //localhost:8080/acd/sad?a=sad&b=sadf
            //localhost/acd/sad?a=sad&b=sadf
            if (splits.Length < 3)
            {
                throw new InvalidUrlStringException(url);
            }
            if (splits[0].Contains(':'))
            {
                string[] hostSplit = splits[0].Split(':');
                Hostname = hostSplit[0];
                Port = hostSplit[1].ToInt32();
            }
            else
            {
                Hostname = splits[0];
                Port = 80;
            }
            BusinessClass = splits[1];
            BusinessAction = splits[2].Split('?').First();
            ParseParameters(Parameters, url);
        }

        private void ParseParameters(Dictionary<string, string> parameters, string url)
        {
            var firstSplit = url.Split('?');
            if (firstSplit.Length < 2)
            {
                return;
            }
            var param = firstSplit[1].Split('&');
            for (int i = 0; i < param.Length; i++)
            {
                var value = param[i].Split('=');
                parameters.Add(value[0], value[1]);
            }
        }
    }
}