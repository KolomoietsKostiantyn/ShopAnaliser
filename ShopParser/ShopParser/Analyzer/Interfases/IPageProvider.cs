using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopParser.Analyzer.Interfases
{
    public interface IPageProvider
    {
        string GetPage(string url);
        string GetPageDescription(string url);
    }
}
