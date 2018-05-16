using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TpORM
{
    public static class ProductsListExtension
    {
        public static List<Product> GetProductsWithoutCategoryImperative(this List<Product> products)
        {
            IEnumerable<Product> en = products.Where(product => product.ProductSubcategory == null);
            return en.ToList();
        }

        public static List<Product> GetProductsWithoutCategoryDeclarative(this List<Product> products)
        {
            IEnumerable<Product> en = from Product p in products
                                      where p.ProductSubcategory == null
                                      select p;
            return en.ToList();
        }

        public static List<Product> GetPagedImperative(this List<Product> products, int pageSize, int pageNumber)
        {
            IEnumerable<Product> en = products.Skip(pageSize * (pageNumber)).Take(pageSize);
            return en.ToList();
        }

        public static string GetProductsWithVendors(this List<Product> products)
        {
            using (var db = new AdventureWorksDataContext())
            {
                var productsVendors = (from Vendor vendor in db.Vendors
                                       join ProductVendor productVendor in db.ProductVendors on vendor.BusinessEntityID equals productVendor.BusinessEntityID
                                       join Product product in db.Products on productVendor.ProductID equals product.ProductID
                                       where products.Select(p => p.ProductID).Contains(product.ProductID)
                                       select new
                                       {
                                           ProductName = product.Name,
                                           VendorName = vendor.Name
                                       }).ToList();

                StringBuilder sb = new StringBuilder();
                foreach (var pv in productsVendors)
                {
                    sb.AppendLine($"{pv.ProductName}-{pv.VendorName}");
                }
                return sb.ToString();
            }
        }
    }
}