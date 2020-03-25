using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IG.TechnicalInterview.Model.Supplier
{
    public class Email
    {
        /// <summary>
        /// Gets or sets the email id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        [Required]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the email is the preferred one or not
        /// </summary>
        public bool IsPreferred { get; set; }

        public bool IsValid()
        {
            return Regex.IsMatch(EmailAddress
                    , @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"
                    , RegexOptions.IgnoreCase);
        }
    }
}
