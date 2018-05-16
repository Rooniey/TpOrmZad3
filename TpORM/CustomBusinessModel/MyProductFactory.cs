using System;
using System.Collections.Generic;

namespace TpORM.CustomBusinessModel
{
    public static class MyProductFactory
    {
        public static MyProduct GetMyProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            MyProduct myProduct = new MyProduct(product.ProductID)
            {
                Name = product.Name,
                StandardCost = product.StandardCost,
                Color = product.Color
            };

            return myProduct;
        }

        public static List<MyProduct> GetMyProducts(List<Product> products)
        {
            var toReturn = new List<MyProduct>();

            foreach (var product in products)
            {
                toReturn.Add(GetMyProduct(product));
            }

            return toReturn;
        }

        public static List<MyProduct> GetMyProductsByName(string namePart)
        {
            var wantedProducts = DataManager.GetProductsByName(namePart);
            return GetMyProducts(wantedProducts);
        }

        public static List<MyProduct> GetMyProductsByVendorName(string vendorName)
        {
            var wantedProducts = DataManager.GetProductsByVendorName(vendorName);
            return GetMyProducts(wantedProducts);
        }

        public static List<MyProduct> GetNMyProductsFromCategory(string categoryName, int n)
        {
            var wantedProducts = DataManager.GetNProductsFromCategory(categoryName, n);
            return GetMyProducts(wantedProducts);
        }
    }
}