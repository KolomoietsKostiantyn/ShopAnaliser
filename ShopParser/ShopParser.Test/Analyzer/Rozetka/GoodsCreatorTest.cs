using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopParser.Analyzer;
using ShopParser.Analyzer.BDWorker;
using Moq;
using ShopParser.Analyzer.JsonParser;
using ShopParser.BD;

namespace ShopParser.Test.Analyzer.Rozetka
{

    [TestClass]
    public class GoodsCreatorTest
    {
        GoodsCreator _goodsCreator;
        [TestMethod]
        public void CreateGoods_NonExistentCategory_Create()
        {
            string imagePath = "1";
            Mock<IDataProvider> dbProvider = new Mock<IDataProvider>();
            RozetkaGoods realGoods = new RozetkaGoods();
            PriceElement priceElement = new PriceElement();
            priceElement.Value = "100 0";
            priceElement.Unit = "2";
            realGoods.Price = priceElement;
            realGoods.Id = "1";
            realGoods.Title = "1";
            realGoods.Social_url = "1";
            realGoods.Price.Unit = "1"; 
            int shopId = 1;
            int categoriesId = 1;
            string description = "sdf";
            _goodsCreator = new GoodsCreator();

            dbProvider.Setup(r => r.GetCurrency(It.IsAny<string>())).Returns<Currency>(null);
            dbProvider.Setup(r => r.AddGoods(It.IsAny<AllGoods>(), It.IsAny<GoodsDescription>()));
            dbProvider.Setup(r => r.AddCurrency(It.IsAny<string>()));
            dbProvider.Setup(r => r.SaveChanges());
            bool result = _goodsCreator.CreateGoods(imagePath, dbProvider.Object, realGoods, shopId, categoriesId, description);

            Assert.IsTrue(result);

            dbProvider.Verify();
        }

    }
}
