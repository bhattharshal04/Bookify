using BookifyNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookifyNew.Interfaces
{
    public interface IProducts
    {
        public Product GetItemById(int itemId);

        public List<Product> GetAllProducts();
    }
}
