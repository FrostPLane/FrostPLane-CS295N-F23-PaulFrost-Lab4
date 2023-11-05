using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using MMABooksEFClasses.MODELS;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {
        
        MMABOOKSCONTEXT dbContext;
        Customer? c;
        Invoice? i;
        State? s;
        List<Customer>? customers;

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABOOKSCONTEXT();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

        [Test]
        public void GetAllTest()
        {
            customers = dbContext.Customers.OrderBy(c => c.CustomerId).ToList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual(1, customers[0].CustomerId);
            PrintAll(customers);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            c = dbContext.Customers.Find(1);
            Assert.IsNotNull(c);
            Assert.AreEqual("Molunguri, A", c.Name);
            Console.WriteLine(c);
        }

        [Test]
        public void GetUsingWhere()
        {
            // get a list of all of the customers who live in OR
            customers = dbContext.Customers.Where(c => c.State.StartsWith("OR")).OrderBy(c => c.State).ToList();
            Assert.AreEqual(5, customers.Count);
            Assert.AreEqual("OR", customers[0].State);
            PrintAll(customers);
        }

        [Test]
        public void GetWithInvoicesTest()
        {
            // get the customer whose id is 20 and all of the invoices for that customer
            c = dbContext.Customers.Include("Invoices").Where(c => c.CustomerId == 20).SingleOrDefault();
            Assert.IsNotNull(c);
            Assert.AreEqual(20, c.CustomerId);
            Assert.AreEqual(3, c.Invoices.Count);
            foreach (var invoice in c.Invoices)
                Console.WriteLine(invoice);
        }

        [Test]
        public void GetWithJoinTest()
        {
            // get a list of objects that include the customer id, name, statecode and statename
            var customers = dbContext.Customers.Join(
               dbContext.States,
               c => c.State,
               s => s.StateCode,
               (c, s) => new { c.CustomerId, c.Name, c.State, s.StateName }).OrderBy(r => r.StateName).ToList();
            Assert.AreEqual(696, customers.Count);
            // I wouldn't normally print here but this lets you see what each object looks like
            foreach (var c in customers)
            {
                Console.WriteLine(c);
            }
        }

        [Test]
        public void DeleteTest()
        {
            c = new Customer();
            c.CustomerId = 701;
            c.Name = "Test, Customer";
            c.Address = "2273 N. Essex L";
            c.City = "Murfreesboro";
            c.State = "T";
            c.ZipCode = "37130";
            dbContext.Customers.Add(c);
            dbContext.SaveChanges();

            c = dbContext.Customers.Find(701);
            dbContext.Customers.Remove(c);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Customers.Find(701));
        }

        [Test]
        public void CreateTest()
        {
            c = new Customer();
            c.CustomerId = 702;
            c.Name = "Test, Customer";
            c.Address = "2273 N. Essex L";
            c.City = "Murfreesboro";
            c.State = "T";
            c.ZipCode = "37130";
            dbContext.Customers.Add(c);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Customers.Find(702));
        }

        [Test]
        public void UpdateTest()
        {
            c = dbContext.Customers.Find(6);
            c.Name = "Test, Name";
            dbContext.Customers.Update(c);
            dbContext.SaveChanges();
            c = dbContext.Customers.Find(6);
            Assert.AreEqual("Test, Name", c.Name);
        }

        public void PrintAll(List<Customer> customers)
        {
            foreach (Customer c in customers)
            {
                Console.WriteLine(c);
            }
        }
        
    }
}