using BookifyNew.Interfaces;
using BookifyNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookifyNew.ViewModel
{
    public class ProductDataContext : IProducts
    {
        private readonly BFDBContext _context;
       
        public ProductDataContext(BFDBContext context)
        {
            _context = context;
        }
        public List<Product> _products { get; set; }
        public Product _product { get; set; }

        public Product GetItemById(int itemId)
        {
            _product = _context.Products.Find(itemId);
            return _product;
        }

        public List<Product> GetAllProducts()
        {
            _products = _context.Products.ToList();
            return _products;
        }
    }
}
