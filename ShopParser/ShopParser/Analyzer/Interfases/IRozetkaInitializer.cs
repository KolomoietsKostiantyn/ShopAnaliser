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
    public interface IRozetkaInitializer
    {
        IPageElementParser GetPageElementSercher();
        IPageProvider PageProvider();
        IPhotoSave PhotoSaver();
        IValidator GetValidator();
        IGoodsCreator GoodsCreator();
        IDataProvider GetWorkerWithBD();
        JParser GetJParser();
        int ShopId { get; }
        string ShopName { get; }
        string ImagePath { get; }
        int ElementOnPage { get; }
    }
}
