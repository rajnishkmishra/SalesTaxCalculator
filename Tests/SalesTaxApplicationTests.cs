using System;
using System.Collections.Generic;
using SalesTax;
using NUnit.Framework;

namespace Tests
{
    public class SalesTaxApplicationTests
    {
        private SalesTaxApplication salesTaxApplication;

        [SetUp]
        public void Setup()
        {
            salesTaxApplication = new SalesTaxApplication();
        }

        [Test]
        public void SuccessTest1()
        {
            //Arrange 
            var shoppingcart = new List<string>()
            {
                "1 book at 12.49",
                "1 music CD at 14.99",
                "1 chocolate bar at 0.85",
            };
            
            
            //Act
            var result = salesTaxApplication.CalculateTaxesAndTotalCost(shoppingcart);
            
            //Assert
            Assert.AreEqual(result.message, Constants.Success);
            Assert.AreEqual(result.Data.Items[0].ProductDescription, "1 book");
            Assert.AreEqual(result.Data.Items[0].TotalPrice, 12.49);
            Assert.AreEqual(result.Data.Items[1].ProductDescription, "1 music CD");
            Assert.AreEqual(result.Data.Items[1].TotalPrice, 16.49);
            Assert.AreEqual(result.Data.Items[2].ProductDescription, "1 chocolate bar");
            Assert.AreEqual(result.Data.Items[2].TotalPrice, 0.85);
            Assert.AreEqual(result.Data.SalesTax, 1.50);
            Assert.AreEqual(result.Data.TotalPrice, 29.83);
        } 

        [Test]
        public void SuccessTest2()
        {
            //Arrange 
            var shoppingcart = new List<string>()
            {
                "1 imported box of chocolates at 10.00",
                "1 imported bottle of perfume at 47.50"
            };
            
            
            //Act
            var result = salesTaxApplication.CalculateTaxesAndTotalCost(shoppingcart);
            
            //Assert
            Assert.AreEqual(result.message, Constants.Success);
            Assert.AreEqual(result.Data.Items[0].ProductDescription, "1 imported box of chocolates");
            Assert.AreEqual(result.Data.Items[0].TotalPrice, 10.50);
            Assert.AreEqual(result.Data.Items[1].ProductDescription, "1 imported bottle of perfume");
            Assert.AreEqual(result.Data.Items[1].TotalPrice, 54.65);
            Assert.AreEqual(result.Data.SalesTax, 7.65);
            Assert.AreEqual(result.Data.TotalPrice, 65.15);
        }

        [Test]
        public void SuccessTest3()
        {
            //Arrange 
            var shoppingcart = new List<string>()
            {
                "1 imported bottle of perfume at 27.99",
                "1 bottle of perfume at 18.99",
                "1 packet of haedache pills at 9.75",
                "1 box of imported chocolates at 11.25"
            };
            
            
            //Act
            var result = salesTaxApplication.CalculateTaxesAndTotalCost(shoppingcart);
            
            //Assert
            Assert.AreEqual(result.message,Constants.Success);
            Assert.AreEqual(result.Data.Items[0].ProductDescription, "1 imported bottle of perfume");
            Assert.AreEqual(result.Data.Items[0].TotalPrice,32.19);
            Assert.AreEqual(result.Data.Items[1].ProductDescription, "1 bottle of perfume");
            Assert.AreEqual(result.Data.Items[1].TotalPrice,20.89);
            Assert.AreEqual(result.Data.Items[2].ProductDescription, "1 packet of haedache pills");
            Assert.AreEqual(result.Data.Items[2].TotalPrice, 9.75);
            Assert.AreEqual(result.Data.Items[3].ProductDescription, "1 imported box of chocolates");
            Assert.AreEqual(result.Data.Items[3].TotalPrice, 11.85);
            Assert.AreEqual(result.Data.SalesTax, 6.70);
            Assert.AreEqual(result.Data.TotalPrice, 74.68);
        }

        [Test]
        public void ErrorTest1()
        {
            //Arrange 
            var shoppingcart = new List<string>()
            {
                "1 imported bottle of perfume at abcd",
                "1 bottle of perfume at 18.99"
            };
            
            
            //Act
            var result = salesTaxApplication.CalculateTaxesAndTotalCost(shoppingcart);
            
            //Assert
            Assert.AreEqual(result.message, Constants.Error);
            Assert.AreEqual(result.Data, null);
        }

        [Test]
        public void ErrorTest2()
        {
            //Arrange 
            var shoppingcart = new List<string>()
            {
                "a box of imported chocolates at 11.25",
                "1 bottle of perfume at 18.99"
            };
            
            
            //Act
            var result = salesTaxApplication.CalculateTaxesAndTotalCost(shoppingcart);
            
            //Assert
            Assert.AreEqual(result.message, Constants.Error);
            Assert.AreEqual(result.Data, null);
        }
    }
}