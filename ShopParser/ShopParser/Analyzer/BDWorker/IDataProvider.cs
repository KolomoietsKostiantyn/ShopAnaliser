using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopParser.Analyzer.BDWorker
{
    public interface IDataProvider
    {
        List<Categories> GetCategoriesWhere(int shopId);
        Categories GetCategory(int shopId, string category);
        CategoriesPath CategoriesPathWhereFirstOrDefault(string category, int shopId);
        AllGoods AllGoodsWhereFirstOrDefault(string IdOnSite, int shopId);
        List<AllGoods> AllGoodsWhere(int shopId, string category, int skipElement, int takeElement);
        int TotalPages(string category, int shopId);
        AllGoods FullGoodsInfo(string idOnSite, int shopId);
        GoodsDescription GetDescription(string idOnSite, int shopId);
        List<ChangeDynamics> GetChangeDynamics(string IdOnSite, int shopId);
        Currency GetCurrency(string name);
        Currency AddCurrency(string name);
        void AddGoods(AllGoods newGoods, GoodsDescription goodsDescription);
        void AddChangeDynamics(ChangeDynamics changeDynamics);
        int SaveChanges();
        void Dispose();
    }
}
