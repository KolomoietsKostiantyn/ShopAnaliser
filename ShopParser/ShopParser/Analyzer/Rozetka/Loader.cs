using ShopParser.Analyzer.Interfases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ShopParser.Analyzer
{
    public class Loader : IPageProvider
    {
        public string GetPageDescription(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            WebRequest request = WebRequest.Create(url + "#tab=characteristics");
            WebResponse response = request.GetResponse();
            string RequestResult = string.Empty;
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                RequestResult = stream.ReadToEnd();
            }
            return RequestResult;
        }

        public string GetPage(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            string RequestResult = string.Empty;
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                RequestResult = stream.ReadToEnd();
            }
            return RequestResult;
        }
    }
}