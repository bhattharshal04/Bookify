using BookifyNew.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookifyNew.Models
{
    public enum SelectedBuyoutOption
    {
        Rent,
        Buy
    }

    public class ProductViewModel
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
        [NotMapped]
        public IFormFile File { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? CategoryId { get; set; }
        public bool IsRentalAvailable { get; set; }
        public bool IsNew { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public SelectedBuyoutOption BuyoutOption { get; set; }

        public string SellerId { get; set; }

        public int ProductIndexDisplay { get; set; }

        public DateTime PostDate { get; set; }

    }

   
}
