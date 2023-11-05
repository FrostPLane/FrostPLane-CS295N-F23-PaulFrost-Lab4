using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using MMABooksEFClasses.MODELS;
using Microsoft.EntityFrameworkCore;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {

        MMABOOKSCONTEXT dbContext;
        Product? p;
        List<Product>? products;

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABOOKSCONTEXT();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

        [Test]
        public void GetAllTest()
        {
            products = dbContext.Products.OrderBy(p => p.ProductCode).ToList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with C# 2010", products[0].Description);
            PrintAll(products);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            p = dbContext.Products.Find("ADV4");
            Assert.IsNotNull(p);
            Assert.AreEqual("Murach's ADO.NET 4 with VB 2010", p.Description);
            Console.WriteLine(p);
        }

        [Test]
        public void GetUsingWhere()
        {
            products = dbContext.Products.Where(p => p.UnitPrice.Equals(56.5000M)).OrderBy(s => s.ProductCode).ToList();
            Assert.AreEqual(7, products.Count);
            Assert.AreEqual("A4CS", products[0].ProductCode);
            PrintAll(products);
        }
        // get a list of all of the products that have a unit price of 56.50


        [Test]
        public void GetWithCalculatedFieldTest()
        {
            // get a list of objects that include the productcode, unitprice, quantity and inventoryvalue
            var products = dbContext.Products.Select(
            p => new { p.ProductCode, p.UnitPrice, p.OnHandQuantity, Value = p.UnitPrice * p.OnHandQuantity }).
            OrderBy(p => p.ProductCode).ToList();
            Assert.AreEqual(16, products.Count);
            foreach (var p in products)
            {
                Console.WriteLine(p);
            }
        }

        [Test]
        public void DeleteTest()
        {
            p = new Product();
            p.ProductCode = "A0B0";
            p.Description = "Test item";
            p.UnitPrice = 56.0000M;
            p.OnHandQuantity = 1001;
            dbContext.Products.Add(p);
            dbContext.SaveChanges();

            p = dbContext.Products.Find("A0B0");
            dbContext.Products.Remove(p);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Products.Find("A0B0"));
        }

        [Test]
        public void CreateTest()
        {
            p = new Product();
            p.ProductCode = "A0B1";
            p.Description = "Test item";
            p.UnitPrice = 56.0000M;
            p.OnHandQuantity = 1001;
            dbContext.Products.Add(p);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Products.Find("A0B1"));
        }

        [Test]
        public void UpdateTest()
        {
            p = dbContext.Products.Find("ADV4");
            p.Description = "TestDesc";
            dbContext.Products.Update(p);
            dbContext.SaveChanges();
            p = dbContext.Products.Find("ADV4");
            Assert.AreEqual("TestDesc", p.Description);
        }

        public void PrintAll(List<Product> products)
        {
            foreach (Product p in products)
            {
                Console.WriteLine(p);
            }
        }

    }
}