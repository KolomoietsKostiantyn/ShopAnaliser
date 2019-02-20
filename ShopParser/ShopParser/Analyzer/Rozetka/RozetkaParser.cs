using ShopParser.Analyzer.Interfases;
using ShopParser.Analyzer.JsonParser;
using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShopParser.Analyzer.BDWorker;

namespace ShopParser.Analyzer
{
    
    public class RozetkaParser: ICertainShopAnalizer
    {
        private IRozetkaInitializer _initializer;
        private IPageElementParser _pageElementParser;
        private IPageProvider _pageProvider;
        private JParser _jParser;
        private int _shopId;
        private string _shopName;
        private IValidator _validator;
        private IPhotoSave _photoSave;
        private IGoodsCreator _goodsCreator;
        private IDataProvider _BD;
        private string _imagePath;
        private int _elementOnPage;
        private int _tryValidatePage = 3;



        public RozetkaParser(IRozetkaInitializer initializer)
        {
            _initializer = initializer;
            _pageElementParser = initializer.GetPageElementSercher();
            _pageProvider = initializer.PageProvider();
            _jParser = initializer.GetJParser();
            _shopId = initializer.ShopId;
            _validator = initializer.GetValidator();
            _photoSave = initializer.PhotoSaver();
            _shopName = initializer.ShopName;
            _goodsCreator = initializer.GoodsCreator();
            _imagePath = initializer.ImagePath;
            _elementOnPage = initializer.ElementOnPage;
            _BD = initializer.GetWorkerWithBD();
        }

        public List<Categories> GetAllCategory()
        {
            List<Categories> categories = _BD.GetCategoriesWhere(_shopId);
            //using (BDConect dBConect = _initializer.CreateNewConect())
            //{
            //    categories = dBConect.Categories.Where(r => r.ShopsId == _shopId).ToList();
            //}
            List<Categories> result = new List<Categories>();
            foreach (Categories item in categories)
            {
                result.Add(new Categories() { Id = item.Id, Name = item.Name, ShopsId = item.ShopsId,  });
            }
            return result;
        }

        public bool ValidateDiapazone(string category, int startPage , int endPage )
        {
            //BDConect dBConect = _initializer.CreateNewConect();
            Categories categories = _BD.GetCategory(_shopId, category); // dBConect.Categories.Where(r => r.Name == category).FirstOrDefault();
            if (categories == null)
            {
                return false;
            }
            CategoriesPath categoriesPath = _BD.CategoriesPathWhereFirstOrDefault(category, _shopId);// dBConect.CategoriesPath.Where(r => r.CategoriesId == categories.Id).FirstOrDefault();
            if (categoriesPath == null)
            {
                return false;
            }
            string carrentLincOnCategory = _pageElementParser.GetFinalPath(_pageProvider, categoriesPath.PathToCategory);
            string page = _pageProvider.GetPage(carrentLincOnCategory + "filter/");
            int totalPage = _pageElementParser.GetPageCount(page);
            DiapasonSelector(ref startPage, ref endPage, totalPage);
            CheckRange(startPage, endPage,  categories, carrentLincOnCategory);
            return true;
        }

        private void CheckRange(int startPage, int endPage,  Categories categories, string carrentLincOnCategory)
        {
            for (int i = startPage; i < endPage; i++)
            {
                string pageNum = string.Format("page={0}/", i);
                string loadedPage = _pageProvider.GetPage(carrentLincOnCategory + "filter/" + pageNum);
                string[] AllLinkOnPage = _pageElementParser.GatAllLinksOnGoods(loadedPage);
                foreach (string item in AllLinkOnPage)
                {
                    RozetkaGoods goodsDescription = GetRozetkaGoods(item);
                    AllGoods allGoods = _BD.AllGoodsWhereFirstOrDefault(goodsDescription.Id,_shopId);// dBConect.AllGoods.Where(r => r.IdOnSite == goodsDescription.Id && r.ShopId == _shopId).FirstOrDefault();
                    if (allGoods != null)
                    {
                        CompareResult result = _validator.Validate(allGoods, goodsDescription);
                        if (result == CompareResult.NedUpdate)
                        {
                            _validator.Update(_BD, allGoods, goodsDescription);
                        }
                    }
                    else
                    {
                        string description = _pageProvider.GetPageDescription(item);
                        _photoSave.Save(goodsDescription.Images.Middle, _shopName + goodsDescription.Id);
                        string descr = TryGetDescription(description);
                        _goodsCreator.CreateGoods(_shopName + goodsDescription.Id + ".jpg", _BD, goodsDescription, _shopId, categories.Id, descr);
                    }
                }
            }
            _BD.SaveChanges();
            _BD.Dispose();
        }

        private RozetkaGoods GetRozetkaGoods(string item)
        {
            RozetkaGoods goodsDescription = null; 
            string page;
            for (int j = 0; j < _tryValidatePage; j++)
            {
                page = _pageProvider.GetPage(item + "#tab=characteristics");
                goodsDescription = _jParser.GetParsed(page);
                if (goodsDescription != null)
                {
                    break;
                }
            }
            return goodsDescription;
        }

        private string TryGetDescription(string description)
        {
            string result = string.Empty;
            for (int i = 0; i < _tryValidatePage; i++)
            {
                result = _pageElementParser.GetDescription(description);
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }

        private void DiapasonSelector(ref int start, ref int end, int maxValue)
        {
            start = Math.Min(start, end);
            end = Math.Max(start, end);
            if (start <= 0 || start >= maxValue)
            {
                start = 1;
            }

            if (end < 0 || end >= maxValue)
            {
                end = maxValue;
            }
        }

        public List<AllGoods> GetPage(int pageNum, string category)
        {
            
            int skipElement = (pageNum - 1) * _elementOnPage;
            List<AllGoods> pageElement = _BD.AllGoodsWhere(_shopId,category, skipElement, _elementOnPage);
            List<AllGoods> result = new List<AllGoods>();
            if (pageElement != null)
            {
                foreach (AllGoods item in pageElement)
                {
                    string Name = (item.ProductName).Replace("!", string.Empty).Replace("?", string.Empty);
                    result.Add(new AllGoods() {IdOnSite = item.IdOnSite, ProductName = Name, Price = item.Price, Id = item.Id, ImagePath = _imagePath + item.ImagePath });
                }
            }

            return result;
        }

        public int PageTotal( string category)
        {
            int result= _BD.TotalPages(category, _shopId);
            return (int)Math.Ceiling((double)result / _elementOnPage);
        }

        public AllGoods FullGoodsInfo(string id)
        {
            AllGoods pageElement = _BD.FullGoodsInfo(id, _shopId);
            pageElement.ProductName = (pageElement.ProductName).Replace("!", string.Empty).Replace("?", string.Empty);
            pageElement.ImagePath = _imagePath + pageElement.ImagePath;
            return pageElement;
        }

        public GoodsDescription GetDescription(string id)
        {
            GoodsDescription goodsDescription = _BD.GetDescription(id, _shopId);
            return goodsDescription;
        }

        public List<ChangeDynamics> GetChangeDynamics(string id)
        {
            List<ChangeDynamics> changeDynamics = _BD.GetChangeDynamics(id, _shopId);
            return changeDynamics;
        }
    }
}