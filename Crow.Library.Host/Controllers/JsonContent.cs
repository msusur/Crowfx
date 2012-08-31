using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace Crow.Library.Host.Controllers
{
    public class JsonContent : StringContent
    {
        public JsonContent(object content)
            : base(Serialize(content))
        {

        }

        private static string Serialize(object content)
        {
            return JsonConvert.SerializeObject(content);
        }
    }
}
