using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookifyNew.Models;
using BookifyNew.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BookifyNew.ViewModel;

namespace BookifyNew.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly BFDBContext _context;
        private readonly UserManager<AppUser> _userManger;

        public OrdersController(BFDBContext context, UserManager<AppUser> userManger)
        {
            _context = context;
            _userManger = userManger;

        }

        // GET: Orders
        public IActionResult YourOrders()
        {
            string userid = _userManger.GetUserId(User);
            List<YourOrdersDetailsViewModel> OrdersDetailsViewModellList = new List<YourOrdersDetailsViewModel>();

            if (String.IsNullOrEmpty(userid))
            {
                return NotFound();
            }
            var OrderList = _context.Orders
                           .AsNoTracking()
                           .AsEnumerable()
                           .Where(x => x.BuyerId == userid)
                           .GroupBy(app => app.guid)
                           .ToList();

            foreach (var orders in OrderList)
            {
                List<Product> ProductList = new List<Product>();
                Order singleOrder = new Order();
                foreach (var selectedorder in orders)
                {
                    var SigleProduct = (from p in _context.Products
                                        where p.ProductId == selectedorder.ProductId
                                        select p).Single();

                    ProductList.Add(SigleProduct);
                    singleOrder = selectedorder;
                }
                var user = _userManger.FindByIdAsync(ProductList.First().SellerId);
                if (user == null)
                {
                    return NotFound();
                }
                AppUser appUser = user.Result;
                YourOrdersDetailsViewModel ordersDetailsViewModel = new YourOrdersDetailsViewModel()
                {
                    product = ProductList,
                    order = singleOrder,
                    appUser = appUser
                };
                OrdersDetailsViewModellList.Add(ordersDetailsViewModel);
            }
            ViewBag.Categories = _context.Categories;
            return View(OrdersDetailsViewModellList);
        }

        // GET: Orders/Details/5
        public IActionResult BuyerDetails(int? id)
        {

            List<MyItemsViewModel> ListBuyerDetails = new List<MyItemsViewModel>();

            if (id == null)
            {
                return NotFound();
            }

            var order = _context.Orders.Where(m => m.ProductId == id);

            foreach (var item in order)
            {
                var user = _userManger.FindByIdAsync(item.BuyerId);
                if (user == null)
                {
                    return NotFound();
                }
                AppUser appUser = user.Result;
                MyItemsViewModel sellerDetailsViewModel = new MyItemsViewModel()
                {
                    order = item,
                    appUser = appUser
                };
                ListBuyerDetails.Add(sellerDetailsViewModel);
            }
            ViewBag.Categories = _context.Categories;
            return View(ListBuyerDetails);
        }

        // GET: Orders/Details/5
        public IActionResult SellerDetails(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Where(m => m.ProductId == id).Single();
            var user = _userManger.FindByIdAsync(product.SellerId);
            if (user == null)
            {
                return NotFound();
            }

            UserDetails userDetails = new UserDetails()
            {
                FirstName = user.Result.FirstName,
                Address = user.Result.Address,
                City = user.Result.City,
                LastName = user.Result.LastName,
                PostalCode = user.Result.PostalCode,
                PhoneNumber = user.Result.PhoneNumber
            };
            ViewBag.Categories = _context.Categories;
            return View(userDetails);
        }

        // GET: Orders/Create
        public async Task<IActionResult> CreateAsync(string BuyoutTotal)
        {
            List<Item> cartCount = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            Guid guid = Guid.NewGuid();
            if (cartCount.Count > 0)
            {
                foreach (var item in cartCount)
                {
                    int? RentId = null;
                    if (item.Product.BuyoutOption.ToString() == "Rent")
                    {
                        Rental rental = new Rental()
                        {
                            ProductId = item.Product.ProductId,
                            RentEndDate = item.Product.EndDate,
                            RentStartDate = item.Product.StartDate
                        };
                        _context.Rentals.Add(rental);
                        await _context.SaveChangesAsync();
                        RentId = rental.RentId;
                    }

                    Order order = new Order()
                    {
                        BuyerId = _userManger.GetUserId(User),
                        OrderDate = DateTime.Now,
                        SellerId = item.Product.SellerId,
                        BuyoutTotal = Convert.ToDecimal(BuyoutTotal),
                        ProductId = item.Product.ProductId,
                        RentEndDate = item.Product.BuyoutOption.ToString() == "Rent" ? item.Product.EndDate : null,
                        RentStartDate = item.Product.BuyoutOption.ToString() == "Rent" ? item.Product.StartDate : null,
                        RentId = RentId,
                        guid = guid
                    };

                    _ = _context.AddAsync(order);
                    await _context.SaveChangesAsync();

                    var Seller = await _userManger.FindByIdAsync(item.Product.SellerId);
                    var Buyer = await _userManger.FindByIdAsync(_userManger.GetUserId(User));
                    if (Seller != null && Buyer != null)
                    {
                        string SellerCode = "Your Product order by " + Buyer.FirstName + " " + Buyer.LastName + " Please Login and check My items for more deatils";
                        string BuyerCode = "Your Product has been placed and here is Seller Details " + Seller.FirstName + " " + Seller.LastName + " Please Login and check Your Orders for more deatils";
                        SmsService smsHelper = new SmsService();
                        _ = smsHelper.SendSmsAsync(Seller.PhoneNumber, SellerCode);
                        _ = smsHelper.SendSmsAsync(Buyer.PhoneNumber, BuyerCode);

                    }
                }
            }
            ViewBag.Categories = _context.Categories;
            return RedirectToAction("ConfirmPage");
        }

        public IActionResult ConfirmPage()
        {
            ViewBag.Categories = _context.Categories;
            HttpContext.Session.Remove("cart");
            return View();
        }

    }
}
