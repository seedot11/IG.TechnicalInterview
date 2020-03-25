using IG.TechnicalInterview.Model.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IG.TechnicalInterview.Tests
{
    internal static class DataHelper
    {
        public static IEnumerable<Supplier> GetDummySuppliers(int numberOfDummies)
        {
            for (var i = 0; i < numberOfDummies; i++)
            {
                yield return new Supplier
                {
                    ActivationDate = DateTime.Now.AddDays(1),
                    FirstName = "Dummy" + i,
                    LastName = "User",
                    Id = Guid.NewGuid(),
                    Title = "Sir",
                    Emails = new List<Email> { new Email { EmailAddress = $"test{i}@test.text"} },
                    Phones = new List<Phone> { new Phone { PhoneNumber = i.ToString()} }
                };
            }
        }

        public static Supplier GetDummySupplier()
        {
            return GetDummySuppliers(1).First();
        }
    }
}
