using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookifyNew.Models
{
    [Keyless]
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long and includes 1 Uppercase, 1 Lowercase and 1 Number", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        
    }
}
