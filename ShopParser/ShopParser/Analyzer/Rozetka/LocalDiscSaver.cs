using ShopParser.Analyzer.Interfases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ShopParser.Analyzer
{
    public class LocalDiscSaver : IPhotoSave
    {
        string _path;

        public LocalDiscSaver(string path)
        {
            _path = path;
        }

        public bool Save(string uri ,string name)
        {
            if (string.IsNullOrWhiteSpace(uri)|| string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            WebClient webClient = new WebClient();
            webClient.DownloadFile(new Uri(uri), HttpContext.Current.Server.MapPath("~"+"/Upload/") + name + ".jpg");

            return true;

        }
    }
}