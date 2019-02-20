using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopParser.Analyzer.Interfases
{
    public interface IPageElementParser
    {
        string GetDescription(string page);
        string GetFinalPath(IPageProvider pageProvider, string longPath);
        int GetPageCount(string page);
        string[] GatAllLinksOnGoods(string page);
        string GetAllPath(string page, string pathPart);
    }
}
