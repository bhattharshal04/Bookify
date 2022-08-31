using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BookifyNew.Models
{
    public partial class Rental
    {
        [Key]
        public int RentId { get; set; }
        public DateTime? RentStartDate { get; set; }
        public DateTime? RentEndDate { get; set; }
        
        public int ProductId { get; set; }

       
    }
}
