using ShopParser.BD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopParser.Models
{
    public class ShopDbInitializer: CreateDatabaseIfNotExists<BDConect>
    {
        protected override void Seed(BDConect db)
        {
            Shops newShops = new Shops() {Name = "Rozetka" };
            Categories newCategories = new Categories() { Name = "notebooks", Shops = newShops };
            CategoriesPath newCategoriesPath = new CategoriesPath() { Categories = newCategories,
                PathToCategory = "https://rozetka.com.ua/|https://rozetka.com.ua/computers-notebooks/|https://rozetka.com.ua/notebooks/" };

            Categories newCategories1 = new Categories() { Name = "mobile-phones", Shops = newShops };
            CategoriesPath newCategoriesPath1 = new CategoriesPath() { Categories = newCategories1,
                PathToCategory = "https://rozetka.com.ua/|https://rozetka.com.ua/telefony-tv-i-ehlektronika/|https://rozetka.com.ua/telefony/|https://rozetka.com.ua/mobile-phones/" };

            db.Shops.Add(newShops);
            db.Categories.Add(newCategories);
            db.CategoriesPath.Add(newCategoriesPath);

            db.Categories.Add(newCategories1);
            db.CategoriesPath.Add(newCategoriesPath1);

            base.Seed(db);
        }

    }
}