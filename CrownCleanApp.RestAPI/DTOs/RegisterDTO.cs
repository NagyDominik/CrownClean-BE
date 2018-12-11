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
        public string Address { get; set; }
    }
}
