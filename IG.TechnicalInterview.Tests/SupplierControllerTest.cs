using IG.TechnicalInterview.Controllers;
using IG.TechnicalInterview.Domain;
using IG.TechnicalInterview.Model.Supplier;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IG.TechnicalInterview.Tests
{
    internal class SupplierControllerTest
    {
        private SuppliersController controller;
        private Mock<ISupplierService> moqSuppliersService;

        [SetUp]
        public void Setup()
        {
            moqSuppliersService = new Mock<ISupplierService>();
            controller = new SuppliersController(moqSuppliersService.Object);
        }

        [Test]
        public void GetSupplierTest()
        {
            var supplier = DataHelper.GetDummySupplier();
            moqSuppliersService.Setup(x => x.GetSupplier(It.IsAny<Guid>()))
                .ReturnsAsync(supplier);

            var response = controller.GetSupplier(supplier.Id).Result;
            Assert.That(response, Is.InstanceOf<ActionResult<Supplier>>());
            Assert.AreEqual(supplier, response.Value);
        }

        [Test]
        public void GetSupplierTest_WhichDoesntExist()
        {
            moqSuppliersService.Setup(x => x.GetSupplier(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var response = controller.GetSupplier(Guid.NewGuid()).Result;
            Assert.That(response.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void GetSuppliersTest()
        {
            var suppliers = DataHelper.GetDummySuppliers(3).ToList();
            moqSuppliersService.Setup(x => x.GetSuppliers())
                .ReturnsAsync(suppliers);

            var response = controller.GetSupplier().Result;

            Assert.That(response, Is.InstanceOf<ActionResult<IEnumerable<Supplier>>>());
            Assert.AreEqual(suppliers, response.Value);
        }

        [Test]
        public void GetSuppliersTest_Empty()
        {
            moqSuppliersService.Setup(x => x.GetSuppliers())
                .ReturnsAsync(() => new List<Supplier>());

            var response = controller.GetSupplier().Result;

            Assert.That(response, Is.InstanceOf<ActionResult<IEnumerable<Supplier>>>());
            Assert.AreEqual(0, response.Value.Count());
        }

        [Test]
        public void PostSuppliersTest()
        {
            var supplier = DataHelper.GetDummySupplier();
            moqSuppliersService.Setup(x => x.InsertSupplier(It.IsAny<Supplier>()));

            var response = controller.PostSupplier(supplier).Result;

            moqSuppliersService.Verify(mock => mock.InsertSupplier(It.IsAny<Supplier>()), Times.Once());
            Assert.That(response, Is.InstanceOf<ActionResult<Supplier>>());
        }

        [Test]
        public void PostSuppliersTest_WithInvalidModel_ActivationDate()
        {
            var supplier = DataHelper.GetDummySupplier();
            supplier.ActivationDate = DateTime.Now;
            var context = new ValidationContext(supplier, null, null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(supplier, context, results, true));
        }

        [TestCase("012345678901")]
        [TestCase("012three45")]
        [TestCase("")]
        public void PostSuppliersTest_WithInvalidModel_WithBadPhones(string phoneNumber)
        {
            var supplier = DataHelper.GetDummySupplier();
            var phone = new Phone();
            phone.PhoneNumber = phoneNumber;
            supplier.Phones.Add(phone);
            var context = new ValidationContext(supplier, null, null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(supplier, context, results, true));
        }

        [TestCase("d@d")]
        [TestCase("datdot")]
        [TestCase("dollup*@*")]
        [TestCase("http://email")]
        public void PostSuppliersTest_WithInvalidModel_WithBadEmails(string emailAddress)
        {
            var supplier = DataHelper.GetDummySupplier();
            var email = new Email();
            email.EmailAddress = emailAddress;
            supplier.Emails.Add(email);
            var context = new ValidationContext(supplier, null, null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(supplier, context, results, true));
        }

        [Test]
        public void PostSuppliersTest_WithInvalidModel()
        {
            var supplier = DataHelper.GetDummySupplier();
            moqSuppliersService.Setup(x => x.InsertSupplier(It.IsAny<Supplier>()));
            controller.ModelState.AddModelError("test", "error");
            var response = controller.PostSupplier(supplier).Result;

            moqSuppliersService.Verify(mock => mock.InsertSupplier(It.IsAny<Supplier>()), Times.Never);
            Assert.That(response.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void DeleteSupplierTest()
        {
            var supplier = DataHelper.GetDummySupplier();
            moqSuppliersService.Setup(x => x.DeleteSupplier(It.IsAny<Guid>()))
                .ReturnsAsync(supplier);

            var response = controller.DeleteSupplier(supplier.Id).Result;
            Assert.That(response, Is.InstanceOf<ActionResult<Supplier>>());
            moqSuppliersService.Verify(mock => mock.DeleteSupplier(supplier.Id), Times.Once());
            Assert.AreEqual(supplier, response.Value);
        }

        [Test]
        public void DeleteSupplierTest_WhichDoesntExist()
        {
            moqSuppliersService.Setup(x => x.DeleteSupplier(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var response = controller.DeleteSupplier(Guid.NewGuid()).Result;           
            moqSuppliersService.Verify(mock => mock.DeleteSupplier(It.IsAny<Guid>()), Times.Once());
            Assert.IsNull(response.Result);
        }
    }
}