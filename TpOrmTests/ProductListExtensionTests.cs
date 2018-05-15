using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TpORM;

namespace TpOrmTests
{
    [TestClass]
    public class ProductListExtensionTests
    {
        private const int numberOfUncategorised = 5;
        private const int numberOfCategorised = 5;
        private List<Product> products;


        [TestInitialize]
        public void Setup()
        {
            products = new List<Product>();
            for (int i = 0; i < numberOfUncategorised; i++)
            { 
                products.Add(new Product()
                {
                    ProductID = i,
                    ProductSubcategory = null
                });
            }
            for (int i = 0; i < numberOfCategorised; i++)
            {
                products.Add(new Product()
                {
                    ProductID = i,
                    ProductSubcategory = new ProductSubcategory()
                    {
                        ProductSubcategoryID = i
                    }
                });
            }

        }


        [TestMethod]
        public void GetProductsWithoutCategoryImperative_ShouldReturnProperValues()
        {
            var number = products.GetProductsWithoutCategoryImperative().Count;
            Assert.AreEqual(numberOfUncategorised, number);
        }

        [TestMethod]
        public void GetProductsWithoutCategoryDeclarative_ShouldReturnProperValues()
        {
            var number = products.GetProductsWithoutCategoryDeclarative().Count;
            Assert.AreEqual(numberOfUncategorised, number);
        }

        [TestMethod]
        public void GetPagedImperative_ShouldReturnProperValues()
        {
            var page = products.GetPagedImperative(5, 0);
            Assert.AreEqual(5, page.Count);
            for (int i = 0; i < 5; i++)
            {
                Assert.IsTrue(page.Contains(products[i]));
            }

            page = products.GetPagedImperative(5, 1);
            Assert.AreEqual(5, page.Count);
            for (int i = 5; i < 10; i++)
            {
                Assert.IsTrue(page.Contains(products[i]));
            }

        }

        [TestMethod]
        public void GetPagedImperative_WhenCalledOnTheLastPage_ShouldReturnTheLastElements()
        {
            var page = products.GetPagedImperative(7, 1);
            Assert.AreEqual(3, page.Count);
            for (int i = 7; i < 10; i++)
            {
                Assert.IsTrue(page.Contains(products[i]));
            }

        }

        //TODO: add product/vendor string test
    }
}
