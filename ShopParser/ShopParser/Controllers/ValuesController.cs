using ShopParser.Analyzer;
using ShopParser.Analyzer.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopParser.Controllers
{
    public class ValuesController : ApiController
    {
        
        private static bool _validationContinue = false;

        public string GetValidation(string shop, string category, int start, int end) 
        {
            if (_validationContinue)
            {
                return "Processing previous request in progress";
            }
            _validationContinue = true;
            IGlobalShopAnalizerInitiator _createCertainShopAnalizer = new GlobalShopAnalizerInitiator();
            ICertainShopAnalizer cAnalizer = _createCertainShopAnalizer.CreateCertainShopAnalizer(shop);
            if (cAnalizer == null)
            {
                _validationContinue = false;
                return "Invalid shop";
            }
            if (!cAnalizer.ValidateDiapazone(category, start, end))
            {
                _validationContinue = false;
                return "Invalid category";
            }
            _validationContinue = false;
            return "Ready";
        }
    }
}
