using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookifyNew.Models;
using System.IO;
using BookifyNew.Helpers;
using Microsoft.AspNetCore.Hosting;
using BookifyNew.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BookifyNew.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly BFDBContext _context;
        IHostingEnvironment env;
        private readonly UserManager<AppUser> _userManger;
        public ProductsController(BFDBContext context, IHostingEnvironment _env, UserManager<AppUser> userManger)
        {
            _context = context;
            env = _env;
            _userManger = userManger;
        }

        private void CleanRental()
        {
            List<Rental> ListRentals = _context.Rentals.Where(rental => rental.RentEndDate.Value.Date > DateTime.Now.Date && rental.RentStartDate.Value.Date < DateTime.Now.Date).ToList();
            _context.Rentals.RemoveRange(ListRentals);
            _context.SaveChanges();
        }

        // GET: Products
        public IActionResult Index()
        {
            CleanRental();
            int tempProductCategory = 0;
            int tempProductIndex = 0;
            List<Product> productsList = new List<Product>();

            var filterBuyProducts = (from p in _context.Products
                                    where !(from r in _context.Orders
                                    select r.ProductId).Contains(p.ProductId)
                                    select p);

            var filterOnlyBuy = from p in _context.Products
                                join o in _context.Orders
                                on p.ProductId equals o.ProductId
                                where o.RentId != null
                                select p;

            var filterRentProduct = from p in filterOnlyBuy
                                    where !(from r in _context.Rentals
                                            select r.ProductId).Contains(p.ProductId)
                                    select p;

            productsList.AddRange(filterBuyProducts);
           
            productsList.AddRange(filterRentProduct);

            List<ProductViewModel> myproductViewModel = new List<ProductViewModel>();

            foreach (var product in productsList)
            {
                Rental rental = _context.Rentals.Where(x => x.ProductId == product.ProductId).FirstOrDefault();

                if (product.CategoryId != tempProductCategory)
                {
                    tempProductCategory = (int)product.CategoryId;
                    tempProductIndex += 1;
                }

                ProductViewModel productViewModel = new ProductViewModel
                {
                    CategoryId = product.CategoryId,
                    File = product.File,
                    ImageName = product.ImagePath,
                    ImagePath = product.ImagePath,
                    IsNew = product.IsNew,
                    IsRentalAvailable = product.IsRentalAvailable,
                    ProductId = product.ProductId,
                    ProductLongDesc = product.ProductLongDesc,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductShortDesc = product.ProductShortDesc,
                    ProductIndexDisplay = tempProductIndex,
                    SellerId = product.SellerId,
                    PostDate = product.PostDate
                };
                myproductViewModel.Add(productViewModel);
            }
            ViewBag.Categories = _context.Categories;
            return View(myproductViewModel);
        }

        public IActionResult FilterProducts(int id)
        {
            List<Product> productsList = new List<Product>();

            var filterBuyProducts = (from p in _context.Products
                                     where !(from r in _context.Orders
                                             select r.ProductId).Contains(p.ProductId)
                                     select p);

            var filterOnlyBuy = from p in _context.Products
                                join o in _context.Orders
                                on p.ProductId equals o.ProductId
                                where o.RentId != null
                                select p;

            var filterRentProduct = from p in filterOnlyBuy
                                    where !(from r in _context.Rentals
                                            select r.ProductId).Contains(p.ProductId)
                                    select p;

            productsList.AddRange(filterBuyProducts);

            productsList.AddRange(filterRentProduct);
            List<ProductViewModel> myproductViewModel = new List<ProductViewModel>();

            foreach (var product in productsList.Where(x=>x.CategoryId == id))
            {
               
                ProductViewModel productViewModel = new ProductViewModel
                {
                    CategoryId = product.CategoryId,
                    File = product.File,
                    ImageName = product.ImagePath,
                    ImagePath = product.ImagePath,
                    IsNew = product.IsNew,
                    IsRentalAvailable = product.IsRentalAvailable,
                    ProductId = product.ProductId,
                    ProductLongDesc = product.ProductLongDesc,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductShortDesc = product.ProductShortDesc,
                    SellerId = product.SellerId,
                    PostDate = product.PostDate
                };
                myproductViewModel.Add(productViewModel);
            }
            ViewBag.Categories = _context.Categories;
            return View(myproductViewModel);
        }

        // GET: Orders
        public IActionResult MyItems()
        {
            string userid = _userManger.GetUserId(User);

            if (String.IsNullOrEmpty(userid))
            {
                return NotFound();
            }
            ViewBag.Categories = _context.Categories;
            return View(_context.Products.Where(x => x.SellerId == userid));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel productViewModel = new ProductViewModel
            {
                CategoryId = product.CategoryId,
                File = product.File,
                ImageName = product.ProductName,
                ImagePath = product.ImagePath,
                IsNew = product.IsNew,
                IsRentalAvailable = product.IsRentalAvailable,
                ProductId = product.ProductId,
                ProductLongDesc = product.ProductLongDesc,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductShortDesc = product.ProductShortDesc,
                ProductIndexDisplay = 0,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                SellerId = product.SellerId,
                PostDate = product.PostDate
            };
            ViewBag.Categories = _context.Categories;
            return View(productViewModel);
        }


        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewBag.Categories = _context.Categories;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                #region Read File Content  

                var uploads = Path.Combine(env.WebRootPath, "uploads");
                bool exists = Directory.Exists(uploads);
                if (!exists)
                    Directory.CreateDirectory(uploads);

                var fileName = Path.GetFileName(product.File.FileName);
                var fileStream = new FileStream(Path.Combine(uploads, product.File.FileName), FileMode.Create);
                string mimeType = product.File.ContentType;
                byte[] fileData = new byte[product.File.Length];
                using (var target = new MemoryStream())
                {
                    product.File.CopyTo(target);
                    fileData = target.ToArray();
                }
                BlobStorageService objBlobService = new BlobStorageService();

                product.ImagePath = objBlobService.UploadFileToBlob(product.File.FileName, fileData, mimeType);
                #endregion
                product.SellerId = _userManger.GetUserId(User);
                product.PostDate = DateTime.Now;
                product.ImageName = product.File.FileName;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewBag.Categories = _context.Categories;
            return View(product);
        }

        //// GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);

        //    return View(product);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductShortDesc,ProductLongDesc,ImageName,ImagePath,ProductPrice,CategoryId,IsRentalAvailable,OrderId,IsNew")] Product product)
        //{
        //    if (id != product.ProductId)
        //    {
        //        return NotFound();
        //    }

        //    int Verify = _context.Orders.Where(x => x.ProductId == id && x.RentId == null).Count();
        //    if (Verify > 0)
        //    {
        //        ModelState.AddModelError(string.Empty, "You can not edit once Product Sold");
        //        return View();
        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(product);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ProductExists(product.ProductId))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
        //    }

        //    return View(product);
        //}

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _context.Categories;
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int Verify = _context.Orders.Where(x => x.ProductId == id && x.RentId == null).Count();
            if (Verify > 0)
            {
                ModelState.AddModelError(string.Empty, "You can not Delete once Product Sold");
                return View();
            }
            else
            {
                var product = await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            ViewBag.Categories = _context.Categories;
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
