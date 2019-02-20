using ShopParser.Analyzer.BDWorker;
using ShopParser.Analyzer.Interfases;
using ShopParser.Analyzer.JsonParser;
using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopParser.Analyzer.Rozetka
{
    public class StandartRozetkaInitializer : IRozetkaInitializer
    {
        public StandartRozetkaInitializer(int shopId, string shopName, string imagePath, int elementOnPage)
        {
            ElementOnPage = elementOnPage;
            ShopId = shopId;
            ShopName = shopName;
            ImagePath = imagePath;
        }

        public int ShopId
        {
            get;
            private set;
        }

        public string ShopName
        {
            get;
            private set;
        }
        public string ImagePath
        {
            get;
            private set;
        }

        public int ElementOnPage
        {
            get;
            private set;
        }

        public IDataProvider GetWorkerWithBD()
        {
            return new BDEntityFrameworkProvider();
        }

        public JParser GetJParser()
        {
            return new JParser();
        }

        public IPageElementParser GetPageElementSercher()
        {
            return new HtmlPageParsser();
        }

        public IValidator GetValidator()
        {
            return new Validator();
        }

        public IGoodsCreator GoodsCreator()
        {
            return new GoodsCreator();
        }

        public IPageProvider PageProvider()
        {
            return new Loader();
        }

        public IPhotoSave PhotoSaver()
        {
            return new LocalDiscSaver(ImagePath);
        }
    }
}