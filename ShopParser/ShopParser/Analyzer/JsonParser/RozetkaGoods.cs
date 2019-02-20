using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopParser.Analyzer.JsonParser
{
    public class RozetkaGoods
    {
        public string Id
        {
            get;
            set;
        }
        public string Social_url
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public ImgVariable Images
        {
            get;
            set;
        }
        public PriceElement Price
        {
            get;
            set;
        }
    }
}