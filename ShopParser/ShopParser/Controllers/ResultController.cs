using HtmlAgilityPack;
using ShopParser.Analyzer;
using ShopParser.Analyzer.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopParser.Controllers
{
    public class ResultController : Controller
    {
        public ActionResult FufullGoodsInfo(string shop, string category)
        {
            IGlobalShopAnalizerInitiator _createCertainShopAnalizer = new GlobalShopAnalizerInitiator();
            ICertainShopAnalizer cAnalizer = _createCertainShopAnalizer.CreateCertainShopAnalizer(shop);
            ViewBag.Goods = cAnalizer.FullGoodsInfo(category);
            ViewBag.GoodsDescription = cAnalizer.GetDescription(category).Specification;

            ViewBag.ChangeDynamics = cAnalizer.GetChangeDynamics(category);
            return View();
        }
    }
}