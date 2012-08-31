using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common.Templating;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Crow.Library.Templating
{
    /// <summary>
    /// C# implementation of Poor Man's Templating for javascript (Douglas Crockford).
    /// </summary>
    internal class PoorMansTemplating : ITemplateEngine
    {
        private static readonly Regex _regex = new Regex("{([^{}]*)}");

        public string EvaluateTemplate(string templateText, object context)
        {
            Dictionary<string, PropertyInfo> propertyList = new Dictionary<string, PropertyInfo>();

            foreach (var item in context.GetType().GetProperties())
            {
                propertyList.Add(item.Name, item);
            }

            MatchCollection matches = _regex.Matches(templateText);
            string key = string.Empty;
            foreach (Match match in matches)
            {
                key = match.Value.Trim('}', '{');
                if (propertyList.ContainsKey(key))
                {
                    templateText = templateText.Replace(match.Value, propertyList[key].GetValue(context, null).ToString());
                }
            }
            return templateText;
        }
    }
}
