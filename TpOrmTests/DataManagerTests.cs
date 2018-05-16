using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TpORM;

namespace TpOrmTests
{
    [TestClass]
    public class DataManagerTests
    {
        [TestMethod]
        public void GetProductsByName_ShouldReturnProperValues()
        {
            var namePart = "Crankarm";
            List<Product> products = DataManager.GetProductsByName(namePart);

            Assert.AreEqual(3, products.Count);

            foreach (var product in products)
            {
                Assert.IsTrue(product.Name.Contains(namePart));
            }
        }

        [TestMethod]
        public void GetProductsByVendorName_ShouldReturnProperValues()
        {
            var vendorName = "Training Systems";
            List<Product> products = DataManager.GetProductsByVendorName(vendorName);

            Assert.AreEqual(3, products.Count);

            int[] ids = { 320, 321, 322 };
            foreach (int id in ids)
            {
                Assert.IsTrue(products.Exists(p => p.ProductID == id));
            }
        }

        [TestMethod]
        public void GetProductNamesByVendorName_ShouldReturnProperValues()
        {
            var vendorName = "Training Systems";
            List<string> products = DataManager.GetProductNamesByVendorName(vendorName);

            Assert.AreEqual(3, products.Count);

            string[] names = { "Chainring Bolts", "Chainring Nut", "Chainring" };
            foreach (string name in names)
            {
                Assert.IsTrue(products.Exists(p => p == name));
            }
        }

        [TestMethod]
        public void GetProductVendorByProductName_ShouldReturnProperValues()
        {
            var productName = "Chainring Bolts";
            string vendor = DataManager.GetProductVendorByProductName(productName);

            Assert.AreEqual("Beaumont Bikes", vendor);
        }

        [TestMethod]
        public void GetProductVendorByProductName_WhenCalledOnNotProvidedProduct_ShouldReturnNull()
        {
            var productName = "ad858974";
            string vendor = DataManager.GetProductVendorByProductName(productName);

            Assert.AreEqual(null, vendor);
        }

        [TestMethod]
        public void GetProductsWithNRecentReviews_ShouldReturnProperValues()
        {
            var numberOfReviews = 1;
            List<Product> products = DataManager.GetProductsWithNRecentReviews(numberOfReviews);

            Assert.AreEqual(2, products.Count);

            int[] ids = { 709, 798 };
            foreach (int id in ids)
            {
                Assert.IsTrue(products.Exists(p => p.ProductID == id));
            }
        }

        [TestMethod]
        public void GetNProductsFromCategory_ShouldReturnProperValues()
        {
            var categoryName = "Clothing";
            var number = 3;
            List<Product> products = DataManager.GetNProductsFromCategory(categoryName, number);

            Assert.AreEqual(3, products.Count);

            int[] ids = { 712, 866, 865 };
            Assert.AreEqual(ids[0], products[0].ProductID);
            Assert.AreEqual(ids[1], products[1].ProductID);
            Assert.AreEqual(ids[2], products[2].ProductID);
        }

        [TestMethod]
        public void GetNProductsFromCategory_WhenCalledOnLessProducts_ShouldReturnThemAll()
        {
            var categoryName = "Clothing";
            var number = 40;
            List<Product> products = DataManager.GetNProductsFromCategory(categoryName, number);

            Assert.AreEqual(35, products.Count);
        }

        [TestMethod]
        public void GetTotalStandardCostByCategory_ShouldReturnProperValues()
        {
            ProductCategory pc = new ProductCategory()
            {
                ProductCategoryID = 3
            };
            int total = DataManager.GetTotalStandardCostByCategory(pc);

            Assert.AreEqual(868, total);
        }

        [TestMethod]
        public void GetNRecentlyReviewedProduct_ShouldReturnProperValues()
        {
            var reviewedProducts = DataManager.GetNRecentlyReviewedProducts(2);

            int[] ids = { 937, 798 };
            foreach (int id in ids)
            {
                Assert.IsTrue(reviewedProducts.Exists(p => p.ProductID == id));
            }
        }

        [TestMethod]
        public void GetNRecentlyReviewedProduct_WhenCalledOnLessProducts_ShouldReturnThemAll()
        {
            var reviewedProducts = DataManager.GetNRecentlyReviewedProducts(4);

            Assert.AreEqual(3, reviewedProducts.Count);
        }

        [TestMethod]
        public void GetTotalStandardCostByCategory_WhenCalledForNoPresentProductsInCategory_ShouldReturnZero()
        {
            ProductCategory pc = new ProductCategory()
            {
                ProductCategoryID = -1
            };
            int total = DataManager.GetTotalStandardCostByCategory(pc);

            Assert.AreEqual(0, total);
        }
    }
}