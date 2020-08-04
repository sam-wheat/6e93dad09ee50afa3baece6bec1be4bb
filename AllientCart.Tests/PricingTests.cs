using AllientCart.Domain;
using AllientCart.Model;
using AllientCart.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace AllientCart.Tests
{
    [TestFixture]
    public class PricingTests
    {
        // In theory we could test ITerminal here however the object we want to test is ProductService because 
        // it is the lower level component that will give us the pricing required by the assignment.

        private readonly IProductService productService;
        private List<Product> cart;

        public PricingTests()
        {
            productService = new ProductService();
        }


        [SetUp]
        public void Setup()
        {
            cart = new List<Product>();
        }

        [Test]
        public void Test1()
        {
            cart.Add(productService.GetProductByID("A"));
            cart.Add(productService.GetProductByID("B"));
            cart.Add(productService.GetProductByID("C"));
            cart.Add(productService.GetProductByID("D"));
            cart.Add(productService.GetProductByID("A"));
            cart.Add(productService.GetProductByID("B"));
            cart.Add(productService.GetProductByID("A"));
            cart.Add(productService.GetProductByID("A"));
            decimal total = productService.GetTotalForCart(cart);
            Assert.AreEqual(32.40m, total);

        }


        [Test]
        public void Test2()
        {
            cart.Add(productService.GetProductByID("C"));
            cart.Add(productService.GetProductByID("C"));
            cart.Add(productService.GetProductByID("C"));
            cart.Add(productService.GetProductByID("C"));
            cart.Add(productService.GetProductByID("C"));
            cart.Add(productService.GetProductByID("C"));
            cart.Add(productService.GetProductByID("C"));
            
            decimal total = productService.GetTotalForCart(cart);
            Assert.AreEqual(7.25m, total);

        }


        [Test]
        public void Test3()
        {
            cart.Add(productService.GetProductByID("A"));
            cart.Add(productService.GetProductByID("B"));
            cart.Add(productService.GetProductByID("C"));
            cart.Add(productService.GetProductByID("D"));

            decimal total = productService.GetTotalForCart(cart);
            Assert.AreEqual(15.40m, total);
        }


        [Test]
        public void Two_multiples_of_four_of_Product_A()
        {
            for(int i=0;i<8;i++)
                cart.Add(productService.GetProductByID("A"));

            decimal total = productService.GetTotalForCart(cart);
            Assert.AreEqual(14m, total);
        }


        [Test]
        public void Two_multiples_of_four_of_Product_A_plus_one()
        {
            for (int i = 0; i < 9; i++)
                cart.Add(productService.GetProductByID("A"));

            decimal total = productService.GetTotalForCart(cart);
            Assert.AreEqual(16m, total);    //($1.75 * 8) + ($2 * 1)
        }
    }
}