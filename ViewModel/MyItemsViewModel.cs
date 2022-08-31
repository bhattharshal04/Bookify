using BookifyNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookifyNew.ViewModel
{
    public class MyItemsViewModel
    {
        public Order order { get; set; }
        public AppUser appUser { get; set; }

        public Product product { get; set; }
    }
}
