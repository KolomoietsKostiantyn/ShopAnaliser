using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopParser.Analyzer.BDWorker
{
    public class BDEntityFrameworkProvider: IDataProvider
    {
        private BDConect dBConect;
        public BDEntityFrameworkProvider()
        {
            dBConect = new BDConect();
        }

        public List<Categories> GetCategoriesWhere(int shopId)
        {
            List<Categories> categories;
            categories = dBConect.Categories.Where(r => r.ShopsId == shopId).ToList();

            return categories;
        }

        public CategoriesPath CategoriesPathWhereFirstOrDefault(string category, int shopId)
        {
            Categories categories = dBConect.Categories.Where(r => r.Name == category && r.ShopsId == shopId).FirstOrDefault();
            return dBConect.CategoriesPath.Where(r => r.CategoriesId == categories.Id).FirstOrDefault();
        }

        public AllGoods AllGoodsWhereFirstOrDefault(string IdOnSite, int shopId)
        {
            return dBConect.AllGoods.Where(r => r.IdOnSite == IdOnSite && r.ShopId == shopId).FirstOrDefault();
        }

        public List<AllGoods> AllGoodsWhere(int shopId, string category, int skipElement, int takeElement)
        {
            Categories categories = dBConect.Categories.Where(i => i.Name == category && i.ShopsId == shopId).FirstOrDefault();
            if (categories == null)
            {
                return null;
            }
            return dBConect.AllGoods.Where(i => i.ShopId == shopId && i.CategoriesId == categories.Id).OrderBy(p => p.Id).Skip(skipElement).Take(takeElement).ToList();
        }

        public int TotalPages(string category, int shopId)
        {
            Categories categories = dBConect.Categories.Where(r => r.ShopsId == shopId && r.Name == category).FirstOrDefault();
            return dBConect.AllGoods.Where(r => r.Categories.Id == categories.Id).Count(); 
        }

        public AllGoods FullGoodsInfo(string idOnSite, int shopId)
        {
            return dBConect.AllGoods.Where(r => r.IdOnSite == idOnSite && r.ShopId == shopId).FirstOrDefault();
        }

        public GoodsDescription GetDescription(string idOnSite, int shopId)
        {
            AllGoods pageElement = dBConect.AllGoods.Where(r => r.IdOnSite == idOnSite && r.ShopId == shopId).FirstOrDefault(); ;
            return dBConect.GoodsDescription.Where(r => r.AllGoodsId == pageElement.Id).FirstOrDefault();
        }

        public List<ChangeDynamics> GetChangeDynamics(string IdOnSite, int shopId)
        {
            AllGoods pageElement = dBConect.AllGoods.Where(r => r.IdOnSite == IdOnSite && r.ShopId == shopId).FirstOrDefault();
            return dBConect.ChangeDynamics.Where(r => r.AllGoodsId == pageElement.Id).ToList();
        }

        public int SaveChanges()
        {
            return dBConect.SaveChanges();
        }

        public void Dispose()
        {
            dBConect.Dispose();
        }

        public Categories GetCategory(int shopId, string category)
        {
            return dBConect.Categories.Where(r => r.ShopsId == shopId && r.Name == category).FirstOrDefault();
        }

        public Currency GetCurrency(string name)
        {
            return dBConect.Currency.Where(r => r.Name == name).FirstOrDefault();
        }

        public Currency AddCurrency(string name)
        {
            Currency currency = new Currency();
            currency.Name = name;
            dBConect.Currency.Add(currency);
            SaveChanges();
            Currency result = dBConect.Currency.Where(r => r.Name == name).FirstOrDefault();
            return result;
        }

        public void AddGoods(AllGoods newGoods, GoodsDescription goodsDescription)
        {
            dBConect.AllGoods.Add(newGoods);
            dBConect.GoodsDescription.Add(goodsDescription);

        }

        public void AddChangeDynamics(ChangeDynamics changeDynamics)
        {
            dBConect.ChangeDynamics.Add(changeDynamics);
            SaveChanges();
        }
    }
}