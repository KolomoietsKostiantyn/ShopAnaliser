using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopParser.Analyzer;
using ShopParser.BD;
using ShopParser.Analyzer.JsonParser;
using Moq;
using ShopParser.Analyzer.BDWorker;

namespace ShopParser.Test.Analyzer.Rozetka
{

    [TestClass]
    public class ValidatorTest
    {
        Validator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new Validator();
        }

        [TestMethod]
        public void Validate_NullReferense_Null()
        {
            AllGoods goods = null;
            RozetkaGoods realGoods = null;

            CompareResult result = _validator.Validate(goods, realGoods);

            CompareResult expected = CompareResult.Null;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Validate_DiferentValue_NedUpdate()
        {

            RozetkaGoods realGoods = new RozetkaGoods();
            PriceElement priceElement = new PriceElement();
            priceElement.Value = "100 0";
            realGoods.Price = priceElement;
            AllGoods bdGoods = new AllGoods();
            bdGoods.Price = 10;

            CompareResult result = _validator.Validate(bdGoods, realGoods);

            CompareResult expected = CompareResult.NedUpdate;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Validate_SameValue_Same()
        {
            RozetkaGoods realGoods = new RozetkaGoods();
            PriceElement priceElement = new PriceElement();
            priceElement.Value = "100 0";
            realGoods.Price = priceElement;
            AllGoods dbGoods = new AllGoods();
            dbGoods.Price = 1000;

            CompareResult result = _validator.Validate(dbGoods, realGoods);

            CompareResult expected = CompareResult.Same;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Update_NullReferense_Null()
        {
            IDataProvider dBConect = null;
            AllGoods goods = null;
            RozetkaGoods realGoods = null;

            AllGoods result = _validator.Update(dBConect, goods, realGoods);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Update_DifferentPrise_CallSave()
        {
            
            AllGoods goods = new AllGoods();
            goods.Price = 100;
            RozetkaGoods realGoods = new RozetkaGoods();
            PriceElement priceElement = new PriceElement();
            priceElement.Value = "100 0";
            realGoods.Price = priceElement;

            Mock<IDataProvider> dbProvider = new Mock<IDataProvider>();
            dbProvider.Setup(r => r.AddChangeDynamics(It.IsAny<ChangeDynamics>()));
            dbProvider.Setup(r => r.SaveChanges());
            AllGoods result = _validator.Update(dbProvider.Object, goods, realGoods);

            int expected = 1000;
            
            Assert.AreEqual(result.Price, expected);
            dbProvider.VerifyAll();
        }

    }
}
