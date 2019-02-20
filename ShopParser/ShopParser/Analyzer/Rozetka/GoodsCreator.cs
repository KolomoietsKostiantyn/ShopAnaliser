using ShopParser.Analyzer.BDWorker;
using ShopParser.Analyzer.Interfases;
using ShopParser.Analyzer.JsonParser;
using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopParser.Analyzer
{
    public class GoodsCreator: IGoodsCreator
    {
        public bool CreateGoods(string imagePath, IDataProvider dBConect, RozetkaGoods realGoods, int shopId, int categoriesId, string description)
        {

            AllGoods newGoods = new AllGoods();
            newGoods.IdOnSite = realGoods.Id;
            newGoods.Price = double.Parse((realGoods.Price.Value).Replace(" ", string.Empty));
            newGoods.ProductName =  realGoods.Title;
            newGoods.ReferenceToTheOriginal = realGoods.Social_url;
            newGoods.ShopId = shopId;
            newGoods.CategoriesId = categoriesId;

            Currency currency = dBConect.GetCurrency(realGoods.Price.Unit); 
            if (currency == null)
            {
                currency = dBConect.AddCurrency(realGoods.Price.Unit);
            }
            newGoods.Currency = currency;
            GoodsDescription goodsDescription = new GoodsDescription();
            goodsDescription.AllGoods = newGoods;
            goodsDescription.Specification = description;
            newGoods.ImagePath = imagePath;
            dBConect.AddGoods(newGoods, goodsDescription);
            dBConect.SaveChanges();

            return true;
        }




    }
}