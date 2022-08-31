using BookifyNew.Helpers;
using BookifyNew.Models;
using BookifyNew.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookifyNew.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly BFDBContext _context;
        public CartController(BFDBContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.ProductPrice);
            }
            ViewBag.Categories = _context.Categories;
            return View();
        }
       
        public IActionResult Buy(int ProductId, DateTime EndDate, DateTime StartDate, SelectedBuyoutOption BuyoutOption)
        {
            ProductDataContext productModel = new ProductDataContext(_context);
            Product selectedItem = productModel.GetItemById(ProductId);

            ProductViewModel productViewModel = new ProductViewModel
            {
                CategoryId = selectedItem.CategoryId,
                File = selectedItem.File,
                ImageName = selectedItem.ImageName,
                ImagePath = selectedItem.ImagePath,
                IsNew = selectedItem.IsNew,
                IsRentalAvailable = selectedItem.IsRentalAvailable,
                ProductId = selectedItem.ProductId,
                ProductLongDesc = selectedItem.ProductLongDesc,
                ProductName = selectedItem.ProductName,
                ProductPrice = selectedItem.ProductPrice,
                ProductShortDesc = selectedItem.ProductShortDesc, 
                EndDate = BuyoutOption.ToString() == "Rent" ?  EndDate : null,
                StartDate = BuyoutOption.ToString() == "Rent" ? StartDate : null,
                BuyoutOption = BuyoutOption,
                SellerId = selectedItem.SellerId
               
            };

            
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = productViewModel });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(productViewModel.ProductId);

                if (index == -1)
                {
                    cart.Add(new Item { Product = productViewModel });
                }
                   
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            ViewBag.Categories = _context.Categories;
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(Convert.ToInt32(id));
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            ViewBag.Categories = _context.Categories;
            return RedirectToAction("Index","Products");
        }

        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
