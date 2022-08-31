using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookifyNew.Models
{
    [Keyless]
    public class TwoFactorViewModel
    {
      
        [Required]
        public string TwoFactorCode { get; set; }

        public string Email { get; set; }

    }
}
