using System;
using System.Collections.Generic;
using System.Linq;

namespace TpORM
{
    public static class DataManager
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                IQueryable<Product> query = db.Products.Where(p => p.Name.Contains(namePart));
                return query.ToList();
            }
        }

        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                IQueryable<Product> query = from Product product in db.Products
                    join ProductVendor productVendor in db.ProductVendors on product.ProductID equals productVendor.ProductID
                    join Vendor vendor in db.Vendors on productVendor.BusinessEntityID equals vendor.BusinessEntityID
                    where vendor.Name == vendorName
                    select product;
                return query.ToList();
            }
        }

        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                IQueryable<string> query = from Product product in db.Products
                    join ProductVendor productVendor in db.ProductVendors on product.ProductID equals productVendor.ProductID
                    join Vendor vendor in db.Vendors on productVendor.BusinessEntityID equals vendor.BusinessEntityID
                    where vendor.Name == vendorName
                    select product.Name;
                return query.ToList();
            }
        }

        //TODO: returns the first one or combines all vendors names into one string
        public static string GetProductVendorByProductName(string productName)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                IQueryable<string> query = from Vendor vendor in db.Vendors
                    join ProductVendor productVendor in db.ProductVendors on vendor.BusinessEntityID equals productVendor.BusinessEntityID
                    join Product product in db.Products on productVendor.ProductID equals product.ProductID
                    where product.Name == productName
                    orderby vendor.Name
                    select vendor.Name;

                return query.FirstOrDefault();
            }
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                IQueryable<Product> query = from Product product in db.Products
                    where (
                              from ProductReview productReview in db.ProductReviews
                              where productReview.ProductID == product.ProductID
                              select productReview
                          ).Count() == howManyReviews
                    select product;
                return query.ToList();
            }
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var query = db.Products.Join(db.ProductReviews,
                        product => product.ProductID,
                        review => review.ProductID,
                        (product, review) => new {Product = product, ReviewDate = review.ReviewDate})
                    .GroupBy(elem => elem.Product.ProductID)
                    .Select(p => p.OrderByDescending(g => g.ReviewDate).First())
                    .OrderByDescending(p => p.ReviewDate)
                    .Select(p=>p.Product)
                    .Take(howManyProducts);

                return query.ToList();
            }
        }
        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                IQueryable<Product> query = from Product product in db.Products
                    join ProductSubcategory subCategory in db.ProductSubcategories on product.ProductSubcategoryID equals subCategory.ProductSubcategoryID
                    join ProductCategory category in db.ProductCategories on subCategory.ProductCategoryID equals category.ProductCategoryID
                    where category.Name == categoryName
                    orderby category.Name, product.Name
                    select product;
                var cos = query.ToString();
                return query.Take(n).ToList();
            }
        }
        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (AdventureWorksDataContext db = new AdventureWorksDataContext())
            {
                var cost = (from Product product in db.Products
                    join ProductSubcategory subCategory in db.ProductSubcategories on product
                        .ProductSubcategoryID equals subCategory.ProductSubcategoryID
                    join ProductCategory pCategory in db.ProductCategories on subCategory
                        .ProductCategoryID equals pCategory.ProductCategoryID
                    where pCategory.ProductCategoryID == category.ProductCategoryID
                    select (decimal?)product.StandardCost).Sum() ?? 0;

                return (int) cost;
            }
        }



    }
}
