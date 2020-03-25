﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace IG.TechnicalInterview.Model.Supplier
{
    public class Supplier : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        [MaxLength(32)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        [MaxLength(64)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        [MaxLength(64)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the activation date
        /// </summary>
        public DateTime ActivationDate { get; set; }

        /// <summary>
        /// Gets or sets Emails for the party
        /// </summary>
        public virtual ICollection<Email> Emails { get; set; } = new HashSet<Email>();

        /// <summary>
        /// Gets or sets Phones for the party
        /// </summary>
        public virtual ICollection<Phone> Phones { get; set; } = new HashSet<Phone>();

        /// <summary>
        /// Supplier validation
        /// </summary>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>List of <see cref="ValidationResult"/></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (ActivationDate < DateTime.Today.AddDays(1))
                validationResults.Add(new ValidationResult("Activation date must be tomorrow or later."));

            Emails.Where(x => !x.IsValid()).ToList()
                .ForEach(y => validationResults
                .Add(new ValidationResult($"{y.EmailAddress} is not a valid email.")));

            Phones.Where(x => !x.IsValid()).ToList()
                .ForEach(y => validationResults
                .Add(new ValidationResult($"{y.PhoneNumber} is not a valid phone number.")));

            return validationResults;
        }
    }
}
