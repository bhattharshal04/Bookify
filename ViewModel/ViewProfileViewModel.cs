using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookifyNew.ViewModel
{
    public class ViewProfileViewModel
    {
        [Key]
        public string Key { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string City { get; set; }

       
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
    }
}
