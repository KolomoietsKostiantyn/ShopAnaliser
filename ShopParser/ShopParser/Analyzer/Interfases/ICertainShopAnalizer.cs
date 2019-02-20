using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopParser.Analyzer.Interfases
{
   public interface ICertainShopAnalizer
    {
        bool ValidateDiapazone(string category, int startPage, int endPage);
        List<Categories> GetAllCategory();
        List<AllGoods> GetPage(int pageNum, string category);
        int PageTotal(string category);
        AllGoods FullGoodsInfo(string id);
        GoodsDescription GetDescription(string id);
        List<ChangeDynamics> GetChangeDynamics(string id);
    }
}
