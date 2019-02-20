using HtmlAgilityPack;
using ShopParser.Analyzer.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ShopParser.Analyzer
{
    public class HtmlPageParsser : IPageElementParser
    {
        char longPathSeparetor = '|';


        private string _regular = @"<span class=\u0022glossary-icon\u0022(.*?)</span>";
        private string _className = "//*[@class=\"chars-t\"]";

        public string GetDescription(string page)
        {
            if (string.IsNullOrWhiteSpace(page))
            {
                return null;
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode(_className);
            string result;
            try
            {
                result = bodyNode.OuterHtml;
            }
            catch (Exception)
            {
                return null;
            }
            
            Regex regex = new Regex(_regular);
            return regex.Replace(result, string.Empty);
        }

        public string GetFinalPath(IPageProvider pageProvider, string longPath)
        {
            string[] links = longPath.Split(longPathSeparetor);
            string result = links[0];

            for (int i = 1; i < links.Length; i++)
            {
                string page = pageProvider.GetPage(result);
                result = GetAllPath(page, links[i]);
            }

            return result;
        }

        public string GetAllPath(string page, string pathPart)
        {
            if (string.IsNullOrWhiteSpace(page) || string.IsNullOrWhiteSpace(pathPart))
            {
                return null;
            }

            string request = pathPart + ".*?/";
            string result =  Regex.Match(page, request).ToString();
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return result;
        }


        public int GetPageCount(string page)
        {
            if (string.IsNullOrWhiteSpace(page))
            {
                throw new ArgumentNullException("Invalid page"); 
            }

            string pageRegular = "total_pages:[0-9]+,";
            Match match = Regex.Match(page, pageRegular);
            if (match.Length == 0)
            {
                throw new ArgumentException();
            }
            string digits = "[0-9]+";
            string onlyDigit = Regex.Match(match.ToString(), digits).ToString();
            return int.Parse(onlyDigit);
        }

        public string[] GatAllLinksOnGoods(string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                return null;
            }
            string serch = "//div[@class='g-i-tile-i-title clearfix']";
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlNodeCollection bodyNode = doc.DocumentNode.SelectNodes(serch);
            if (bodyNode == null || bodyNode.Count == 0)
            {
                throw new ArgumentException("many or few elements containing: " + serch);
            }
            string[] result = new string[bodyNode.Count];

            for (int i = 0; i < bodyNode.Count; i++)
            {
                result[i] = GetLinc(bodyNode[i].InnerHtml);
            }
            return result;
        }

        public string GetLinc(string inner)
        {
            if (string.IsNullOrEmpty(inner))
            {
                return null;
            }
            string request = "https://rozetka.com.ua/.*?/.*?/";
            return Regex.Match(inner, request).ToString();
        }
    }
}