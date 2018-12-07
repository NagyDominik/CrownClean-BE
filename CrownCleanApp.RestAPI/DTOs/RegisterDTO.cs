using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrownCleanApp.RestAPI.DTOs
{
    public class RegisterDTO
    {
        /// <summary>
        /// Custom validation attribute to validate the address list.
        /// </summary>
        class ListValidationAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var list = value as IList;
                if (list == null)
                {
                    return false;
                }
                return list.Count > 0;
            }
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public bool IsCompany { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [ListValidation(ErrorMessage = "At least one address is required!")]
        public List<string> Addresses { get; set; }
    }
}
