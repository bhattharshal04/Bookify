using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookifyNew.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        public string Address { get; set; }
    }
}
