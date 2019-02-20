using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ShopParser.Analyzer.JsonParser
{
    public class JParser
    {
        private char openBracket = '{';
        private char closedBracket = '}';
        string JavaScriptClassName = "RozetkaStickyGoods_class.prototype.options.goods";

        public RozetkaGoods GetParsed(string page)
        {
            if (string.IsNullOrWhiteSpace(page))
            {
                return null;
            }

            string[] allStrings = page.Split('\n');
            string result = string.Empty;
            for (int i = 0; i < allStrings.Length; i++)
            {
                if (allStrings[i].Contains(JavaScriptClassName))
                {
                    result += allStrings[i];
                    int openingbracket = allStrings[i].Split(openBracket).Length - 1;
                    int closingbracket = allStrings[i].Split(closedBracket).Length - 1;
                    while (openingbracket != closingbracket)
                    {
                        i++;
                        openingbracket += allStrings[i].Split(openBracket).Length - 1;
                        closingbracket += allStrings[i].Split(closedBracket).Length - 1;
                        result += allStrings[i];
                    }
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(result))
            {
                return null;
            }
            result = ReplaseAditionalSigns(result);
            JToken token = JObject.Parse(result);
            return JsonConvert.DeserializeObject<RozetkaGoods>(result);
        }

        private string ReplaseAditionalSigns(string inner)
        {
            inner = inner.Replace("RozetkaStickyGoods_class.prototype.options.goods = ", string.Empty);
            inner = inner.Replace(";", string.Empty);
            inner = inner.Replace("\\", "\\\\");
            return inner;
        }
    }
}