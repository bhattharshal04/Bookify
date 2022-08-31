using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BookifyNew.Models
{
    public partial class Product
    {
       
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductShortDesc { get; set; }
        public string ProductLongDesc { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ImageName { get; set; }

        [Column(TypeName = "varchar(250)")]
       
        public string ImagePath { get; set; }
        [Required]
        [NotMapped]
        public IFormFile File { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? CategoryId { get; set; }
        public bool IsRentalAvailable { get; set; }
        public bool IsNew { get; set; }

        public string SellerId { get; set; }

        public DateTime PostDate { get; set; }

       
    }
}
