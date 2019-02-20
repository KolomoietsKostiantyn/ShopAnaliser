using ShopParser.Analyzer.BDWorker;
using ShopParser.Analyzer.Interfases;
using ShopParser.Analyzer.JsonParser;
using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopParser.Analyzer
{
    public class Validator: IValidator
    {

        public CompareResult Validate(AllGoods goods, RozetkaGoods realGoods)
        {
            if (goods == null || realGoods == null)
            {
                return CompareResult.Null;
            }

            string realPrise = (realGoods.Price.Value).Replace(" ", string.Empty);
            double prise;
            if (!double.TryParse(realPrise, out prise))
            {
                return CompareResult.Null;
            }
            if (goods.Price != prise)
            {
                return CompareResult.NedUpdate;
            }

            return CompareResult.Same;
        }

        public AllGoods Update(IDataProvider dBConect, AllGoods goods, RozetkaGoods realGoods)
        {
            if (goods == null || realGoods == null || dBConect == null)
            {
                return null;
            }

            string realPrise = (realGoods.Price.Value).Replace(" ", string.Empty);
            double prise;
            if (!double.TryParse(realPrise, out prise))
            {
                return null;
            }

            if (goods.Price != prise)
            {
                double oldValue = goods.Price;
                goods.Price = prise;
                ChangeDynamics changeDynamics = new ChangeDynamics();
                changeDynamics.NewPrice = prise;
                changeDynamics.OldPrice = oldValue;
                changeDynamics.UpdateTime = DateTime.Now;
                changeDynamics.AllGoodsId = goods.Id;
                dBConect.AddChangeDynamics(changeDynamics);
                dBConect.SaveChanges();
            }
            return goods;
        }

    }
}