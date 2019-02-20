using ShopParser.Analyzer.BDWorker;
using ShopParser.Analyzer.JsonParser;
using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopParser.Analyzer.Interfases
{
    public interface IGoodsCreator
    {
        bool CreateGoods(string ImagePath, IDataProvider dBConect, RozetkaGoods realGoods, int shopId, int categoriesId,  string description);
    }
}
