using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BookifyNew.Models
{
    public partial class Order
    {
       
        [Key]
        public int OrderId { get; set; }
        public string BuyerId { get; set; }
        public string SellerId { get; set; }

        public DateTime? RentStartDate { get; set; }
        public DateTime? RentEndDate { get; set; }
        public decimal BuyoutTotal { get; set; }

        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public int? RentId { get; set; }

        public Guid guid { get; set; }
    }
}
