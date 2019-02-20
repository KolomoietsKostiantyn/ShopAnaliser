using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopParser.Analyzer.Interfases
{
    interface IGlobalShopAnalizerInitiator
    {
        ICertainShopAnalizer CreateCertainShopAnalizer(string shopName);
    }
}
