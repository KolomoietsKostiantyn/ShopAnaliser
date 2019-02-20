using ShopParser.Analyzer.Interfases;
using ShopParser.Analyzer.Rozetka;
using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopParser.Analyzer
{
    public class GlobalShopAnalizerInitiator : IGlobalShopAnalizerInitiator
    {
        string imagePath = "/Upload/";
        int elementOnPage = 10;

        public ICertainShopAnalizer CreateCertainShopAnalizer(string shopName)
        {
            BDConect bDConect = new BDConect();
            Shops shop = bDConect.Shops.Where(r => r.Name == shopName).FirstOrDefault();
            bDConect.Dispose();
            if (shop != null && shop.Name == "Rozetka")
            {
                StandartRozetkaInitializer initializer = new StandartRozetkaInitializer(shop.Id, shop.Name, imagePath, elementOnPage);

                return new RozetkaParser(initializer);
            }

            return null;
        }
    }
}