using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TpORM.CustomBusinessModel;

namespace TpOrmTests
{
    [TestClass]
    public class AdaptingFactoryTests
    {
        [TestMethod]
        public void GetMyProductsByName_ShouldReturnProperValues()
        {
            var namePart = "Crankarm";
            List<MyProduct> products = MyProductFactory.GetMyProductsByName(namePart);

            Assert.AreEqual(3, products.Count);

            foreach (var product in products)
            {
                Assert.IsTrue(product.Name.Contains(namePart));
            }
        }

        [TestMethod]
        public void GetMyProductsByVendorName_ShouldReturnProperValues()
        {
            var vendorName = "Training Systems";
            List<MyProduct> products = MyProductFactory.GetMyProductsByVendorName(vendorName);

            Assert.AreEqual(3, products.Count);

            int[] ids = { 320, 321, 322 };
            foreach (int id in ids)
            {
                Assert.IsTrue(products.Exists(p => p.ProductID == id));
            }
        }

        [TestMethod]
        public void GetNMyProductsFromCategory_ShouldReturnProperValues()
        {
            var categoryName = "Clothing";
            var number = 3;

            List<MyProduct> products = MyProductFactory.GetNMyProductsFromCategory(categoryName, number);

            Assert.AreEqual(3, products.Count);

            int[] ids = { 712, 866, 865 };
            Assert.AreEqual(ids[0], products[0].ProductID);
            Assert.AreEqual(ids[1], products[1].ProductID);
            Assert.AreEqual(ids[2], products[2].ProductID);
        }

        [TestMethod]
        public void GetNMyProductsFromCategory_WhenCalledOnLessProducts_ShouldReturnThemAll()
        {
            var categoryName = "Clothing";
            var number = 40;
            List<MyProduct> products = MyProductFactory.GetNMyProductsFromCategory(categoryName, number);

            Assert.AreEqual(35, products.Count);
        }
    }
}