using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IG.TechnicalInterview.Model.Supplier
{
    public class Phone
    {
        /// <summary>
        /// Gets or sets the phone id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the email is the preferred one or not
        /// </summary>
        public bool IsPreferred { get; set; }

        public bool IsValid()
        {
            return PhoneNumber.Length <= 10 && Regex.IsMatch(PhoneNumber, @"^[0-9]+$");
        }
    }
}