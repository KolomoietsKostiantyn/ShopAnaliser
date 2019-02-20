using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopParser.Analyzer.JsonParser;


namespace ShopParser.Test.Analyzer.Rozetka.JsonParser
{

    [TestClass]
    public class JParserTest
    {
        private JParser _jParser;
        [TestInitialize]
        public void TestInitialize()
        {
            _jParser = new JParser();
        }

        [TestMethod]
        public void GetParsed_NullInner_Null()
        {
            string inner = null;

            RozetkaGoods result = _jParser.GetParsed(inner);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetParsed_NullStringHaveNoJsClass_Null()
        {
            string inner = "< !doctype html > < html > < head > </ head > < body > " +
                    " < table class=\"chars-t\"><tr><td class=\"chars-t\"><div class=\"chars-title\">" +
                    "Страна-производитель товара</div> </td></table></body></html>";

            RozetkaGoods result = _jParser.GetParsed(inner);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetParsed_ValidData_Ok()
        {
            string inner = "< !doctype html > < html > < head > </ head > < body > " +
                    " < table class=\"chars-t\"><tr><td class=\"chars-t\"><div class=\"chars-title\">" +
                    "Страна-производитель товара</div>\n" +
                    "RozetkaStickyGoods_class.prototype.options.goods = {\n" +
                    "id: 63291506,\n" +
                    "social_url: 'https://rozetka.com.ua/prestigio_psb141c01bfh_db_cis/p63291506/',\n" +
                    "title: 'Ноутбук Prestigio SmartBook 141C (PSB141C01BFH_DB_CIS) Dark Blue Суперцена!!!',\n" +
                    "images:\n" +
                    "{\n" +
                    "original: 'https://i2.rozetka.ua/goods/9002685/prestigio_psb141c01bfh_db_cis_images_9002685704.jpg',\n" +
                    "middle: 'https://i1.rozetka.ua/goods/9002686/prestigio_psb141c01bfh_db_cis_images_9002686082.jpg'\n" +
                    "},\n" +
                    "status:\n" +
                    "{\n" +
                    "title: 'Товар заканчивается',\n" +
                    "value: 'limited',\n" +
                    "status_value: 'active'\n" +
                    "},\n" +
                    "state: 'new',\n" +
                    "price:\n" +
                    "{\n" +
                    "value: '4 999',\n" +
                    "unit: 'грн'\n" +
                    "}\n" +
                    "};\n" +
                    "\n</td></table></body></html>";

            RozetkaGoods result = _jParser.GetParsed(inner);

            RozetkaGoods expected = new RozetkaGoods();
            expected.Id = "63291506";
            expected.Images = new ImgVariable() {Middle = "https://i1.rozetka.ua/goods/9002686/prestigio_psb141c01bfh_db_cis_images_9002686082.jpg",
                                                 Original = "https://i2.rozetka.ua/goods/9002685/prestigio_psb141c01bfh_db_cis_images_9002685704.jpg"};
            expected.Price = new PriceElement() { Unit = "грн" , Value = "4 999" };
            expected.Social_url = "https://rozetka.com.ua/prestigio_psb141c01bfh_db_cis/p63291506/";
            expected.Title = "Ноутбук Prestigio SmartBook 141C (PSB141C01BFH_DB_CIS) Dark Blue Суперцена!!!";

            Assert.AreEqual(result.Id, expected.Id);
            Assert.AreEqual(result.Images.Middle, expected.Images.Middle);
            Assert.AreEqual(result.Images.Original, expected.Images.Original);
            Assert.AreEqual(result.Price.Unit, expected.Price.Unit);
            Assert.AreEqual(result.Price.Value, expected.Price.Value);
            Assert.AreEqual(result.Social_url, expected.Social_url);
            Assert.AreEqual(result.Title, expected.Title);

        }

    }
}
