using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopParser.Analyzer;
using Moq;
using ShopParser.Analyzer.Interfases;

namespace ShopParser.Test.Analyzer.Rozetka
{

    [TestClass]
    public class HtmlPageParsserTest
    {
        HtmlPageParsser _htmlPageParsser;

        [TestInitialize]
        public void TestInitialize()
        {
            _htmlPageParsser = new HtmlPageParsser();
        }

        [TestMethod]
        public void GetDescription_NullInner_Null()
        {
            string inner = null;

            string result = _htmlPageParsser.GetDescription(inner);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetDescription_HaveClassHacharsT_String()
        {
            string inner = "< !doctype html > < html > < head > </ head > < body > " +
                    " < table class=\"chars-t\"><tr><td class=\"chars-t\"><div class=\"chars-title\">" +
                    "Страна-производитель товара</div> </td></table></body></html>";

            string result = _htmlPageParsser.GetDescription(inner);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDescription_DeleteSpanElement_Valid()
        {
            string inner = "< !doctype html > < html > < head > </ head > < body > " +
             " < table class=\"chars-t\"><tr><td class=\"chars-t\"><div class=\"chars-title\">" +
             "Страна-производитель товара<span class=\"glossary-icon\">asd</span></div> </td></table></body></html>";

            string expected = "<td class=\"chars-t\"><div class=\"chars-title\">Страна-производитель товара</div> </td>";
            string result = _htmlPageParsser.GetDescription(inner);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAllPath_NullPage_Null()
        {
            string page = null;
            string pathPart = "https://rozetka.com.ua/computers-notebooks/";

            string result = _htmlPageParsser.GetAllPath(page, pathPart);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllPath_NullPath_Null()
        {
            string page = "https://rozetka.com.ua/computers-notebooks/";
            string pathPart = null;

            string result = _htmlPageParsser.GetAllPath(page, pathPart);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllPath_ValidData_Ok()
        {
            string page = "< !doctype html > < html > < head > </ head > < body > " +
             " https://rozetka.com.ua/computers-notebooks/c80253/ </body></html>";
            string pathPart = "https://rozetka.com.ua/computers-notebooks/";

            string result = _htmlPageParsser.GetAllPath(page, pathPart);
            string expected = "https://rozetka.com.ua/computers-notebooks/c80253/";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GetFinalPath_ValidData_Ok()
        {
            string innerLongPath = "https://rozetka.com.ua/|https://rozetka.com.ua/computers-notebooks/";
            string page1 = "<!doctype html>< html >< head ></ head >< body >" +
                " https://rozetka.com.ua/computers-notebooks/c80253/ </body></ html > ";



            Mock<IPageProvider> _iPageProvider = new Mock<IPageProvider>();
            _iPageProvider.Setup(r => r.GetPage(It.Is<string>(val => val == "https://rozetka.com.ua/"))).Returns(page1);
            string result = _htmlPageParsser.GetFinalPath(_iPageProvider.Object, innerLongPath);

            string expected = "https://rozetka.com.ua/computers-notebooks/c80253/";

            Assert.AreEqual(result, expected);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPageCount_NullPage_Exeption()
        {
            string inner = null;

            int result = _htmlPageParsser.GetPageCount(inner);
        }

        [TestMethod]
        public void GetPageCount_ValidData_Ok()
        {
            string inner = "< !doctype html > < html > < head > </ head > < body > " +
             " total_pages:88,3 </body></html>";

            int result = _htmlPageParsser.GetPageCount(inner);

            int expected = 88;
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPageCount_InvalidData_Exeption()
        {
            string inner = "< !doctype html > < html > < head > </ head > < body > " +
             " total_pages:88 ,3 </body></html>";

            int result = _htmlPageParsser.GetPageCount(inner);
        }


        [TestMethod]
        public void GatAllLinksOnGoods_NullPage_Ok()
        {
            string inner = null;

            string[] result = _htmlPageParsser.GatAllLinksOnGoods(inner);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GatAllLinksOnGoods_NullhLink_Exeption()
        {
            string inner = "< !doctype html > < html > < head > </ head > < body > " +
             " asdd ff ff" +
             "</body></html>";

            string[] result = _htmlPageParsser.GatAllLinksOnGoods(inner);
        }

        [TestMethod]
        public void GatAllLinksOnGoods_ValidData_Ok()
        {
            string inner = "< !doctype html > < html > < head > </ head > < body > " +
             " <div class='g-i-tile-i-title clearfix'>https://rozetka.com.ua/prestigio_psb141c01bfh_db_cis/p63291506/ </div>" +
            "<div class='g-i-tile-i-title clearfix'>https://rozetka.com.ua/hp_4lt72es/p56263344/</div>" +
            "</body></html>";

            string[] result = _htmlPageParsser.GatAllLinksOnGoods(inner);

            string[] expected = new string[] { "https://rozetka.com.ua/prestigio_psb141c01bfh_db_cis/p63291506/",
                                                "https://rozetka.com.ua/hp_4lt72es/p56263344/"};

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GetLinc_ValidData_Ok()
        {
            string inner = "< !doctypeml > < head > </ head > < body > " +
             " <div class='g-i-itle clearfix'>https://rozetka.cotigio_psb14106/ </div>" +
            "<div class-i-tile-i clearfix'>https://rozetka.com.ua/hp_4lt72es/p56263344/</div>" +
            "</body";

            string result = _htmlPageParsser.GetLinc(inner);

            string expected = "https://rozetka.com.ua/hp_4lt72es/p56263344/";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GetLinc_NullInnerText_Null()
        {
            string inner = null;

            string result = _htmlPageParsser.GetLinc(inner);

            Assert.IsNull(result);
        }




    }
}
