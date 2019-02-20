using ShopParser.Analyzer;
using ShopParser.Analyzer.Interfases;
using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopParser.Controllers
{
    public class HomeController : Controller
    {
        string shop = "Rozetka";
        public ActionResult Index()
        {
            IGlobalShopAnalizerInitiator _createCertainShopAnalizer = new GlobalShopAnalizerInitiator();
            ICertainShopAnalizer cAnalizer = _createCertainShopAnalizer.CreateCertainShopAnalizer(shop);
            ViewBag.Category = cAnalizer.GetAllCategory();
            ViewBag.ShopName = shop;
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult UpdateCategory(string shop, string category)
        {

            ViewBag.ShopName = shop;
            ViewBag.Category = category;

            return View();
        }



        public ActionResult ViewDetails(string shop, string category)
        {
            ViewBag.ShopName = shop;
            ViewBag.Category = category;
            IGlobalShopAnalizerInitiator _createCertainShopAnalizer = new GlobalShopAnalizerInitiator();
            ICertainShopAnalizer cAnalizer = _createCertainShopAnalizer.CreateCertainShopAnalizer(shop);
            ViewBag.Pages = cAnalizer.PageTotal(category);

            return View();
        }

        public ActionResult GetResultPage(string shop, string category, int page)
        {
            IGlobalShopAnalizerInitiator _createCertainShopAnalizer = new GlobalShopAnalizerInitiator();
            ICertainShopAnalizer cAnalizer = _createCertainShopAnalizer.CreateCertainShopAnalizer(shop);
            ViewBag.Goods = cAnalizer.GetPage(page, category);
            ViewBag.ShopName = shop;
            ViewBag.Category = category;
            return PartialView();
        }

        public ActionResult Instruction()
        {

            ViewBag.Title = "Instruction";

            return View();
        }
    }
}
