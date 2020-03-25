using IG.TechnicalInterview.Data.Context;
using IG.TechnicalInterview.Model.Supplier;
using IG.TechnicalInterview.Tests;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IG.TechnicalInterview.Domain.Tests
{
    public class SupplierServiceTests
    {
        private DbContextOptions<SupplierContext> options;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<SupplierContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            SetupDatabase();
        }

        [Test]
        public void Get()
        {
            using (var context = new SupplierContext(options))
            {
                var service = new SupplierService(context);

                var result = service.GetSupplier(context.Suppliers.First().Id).Result;

                Assert.AreEqual(context.Suppliers.First(), result);
            }
        }

        [Test]
        public void GetAll()
        {
            using (var context = new SupplierContext(options))
            {
                var service = new SupplierService(context);

                var result = service.GetSuppliers().Result;

                Assert.AreEqual(context.Suppliers, result);
            }
        }

        [Test]
        public void Post()
        {
            using (var context = new SupplierContext(options))
            {
                var service = new SupplierService(context);
                var supplier = DataHelper.GetDummySupplier();
                supplier.Id = Guid.NewGuid();
                var result = service.InsertSupplier(supplier);

                Assert.AreEqual(4, context.Suppliers.Count());
            }
        }

        private void SetupDatabase()
        {
            using (var context = new SupplierContext(options))
            {
                var suppliers = DataHelper.GetDummySuppliers(3);
                context.AddRange(suppliers);

                context.SaveChanges();
            }
        }
    }
}