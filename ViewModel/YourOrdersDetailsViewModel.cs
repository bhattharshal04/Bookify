using BookifyNew.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookifyNew.ViewModel
{
    [Keyless]
    public class YourOrdersDetailsViewModel
    {
        public Order order { get; set; }
        public AppUser appUser { get; set; }

        public List<Product> product { get; set; }
    }
}
