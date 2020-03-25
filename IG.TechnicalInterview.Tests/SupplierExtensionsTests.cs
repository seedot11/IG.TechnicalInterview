using IG.TechnicalInterview.Model.Extensions;
using IG.TechnicalInterview.Model.Supplier;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IG.TechnicalInterview.Tests
{
    internal class SupplierExtensionsTests
    {
        [Test]
        public void Supplier_IsActive()
        {
            var supplier = new Supplier();
            supplier.ActivationDate = DateTime.Now.AddDays(-1);
            Assert.IsTrue(supplier.IsActive());
        }

        [Test]
        public void Supplier_IsNotActive()
        {
            var supplier = new Supplier();
            supplier.ActivationDate = DateTime.Now.AddDays(1);
            Assert.IsFalse(supplier.IsActive());
        }
    }
}
