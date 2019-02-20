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
    public interface IValidator
    {
        CompareResult Validate(AllGoods goods, RozetkaGoods realGoods);
        AllGoods Update(IDataProvider dBConect, AllGoods goods, RozetkaGoods realGoods);
    }
}
