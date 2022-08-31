using BookifyNew.Helpers;
using BookifyNew.Models;
using BookifyNew.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace BookifyNew.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly BFDBContext _context;

        private readonly IConfiguration _configuration;
        public AccountController(UserManager<AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager, BFDBContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            _configuration = configuration;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(model.Email);
                if (userCheck == null)
                {
                    var user = new AppUser() { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber,City = model.City,Address = model.Address, PostalCode = model.PostalCode,FirstName = model.FirstName,LastName = model.LastName };


                    var result = await userManager.CreateAsync(user, model.Password);


                    if (result.Succeeded)
                    {
                        
                        var userResult = await userManager.FindByEmailAsync(model.Email);
                        var userId = await userManager.GetUserIdAsync(userResult);
                        var code = await userManager.GenerateEmailConfirmationTokenAsync(userResult);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        return RedirectToAction("LoginTwoStep", new { email = user.Email });
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(model);
                }
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> ViewProfileAsync()
        {
            ViewProfileViewModel model = new ViewProfileViewModel();
            var userCheck = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userCheck != null)
            {
                var userId = await userManager.FindByIdAsync(userCheck);
                ViewProfileViewModel FindByEmailAsync = new ViewProfileViewModel()
                {
                    Address = userId.Address,
                    City = userId.City,
                    Email = userId.Email,
                    FirstName = userId.FirstName,
                    Key = userId.Id,
                    LastName = userId.LastName,
                    PhoneNumber = userId.PhoneNumber,
                    PostalCode = userId.PostalCode
                };
                model = FindByEmailAsync;
            }
            ViewBag.Categories = _context.Categories;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ViewProfile(ViewProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(model.Email);
                if (userCheck != null)
                {
                    userCheck.UserName = model.Email;
                    userCheck.Email = model.Email;
                    userCheck.PhoneNumber = model.PhoneNumber;
                    userCheck.City = model.City;
                    userCheck.Address = model.Address;
                    userCheck.PostalCode = model.PostalCode;
                    userCheck.FirstName = model.FirstName;
                    userCheck.LastName = model.LastName ;


                    var result = await userManager.UpdateAsync(userCheck);


                    if (result.Succeeded)
                    {

                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Can not find email.");
                    return View(model);
                }
            }
            ViewBag.Categories = _context.Categories;
            return View(model);

        }

        //[HttpGet, AllowAnonymous]
        //public async Task<IActionResult> EnableTwoFactor(string email, string code)
        //{
        //    var user = await userManager.FindByEmailAsync(email);
        //    EnableFactor model = new EnableFactor();
        //    if (user != null)
        //    {
        //        model.Id = user.Id;
        //        model.EmailConfirmed = user.EmailConfirmed;
        //        model.TwoFactorEnabled = user.TwoFactorEnabled;
        //        model.Email = user.Email;
        //        model.code = code;
        //    }
        //    else
        //    {

        //        ModelState.AddModelError("message", "Email not Found");
        //    }

        //    return View(model);
        //}

        //[HttpPost, AllowAnonymous]
        //public async Task<IActionResult> EnableTwoFactor(EnableFactor model)
        //{

        //    var userCheck = await userManager.FindByIdAsync(model.Id);
        //    if (userCheck != null)
        //    {

        //        if (model.Id == null || model.code == null)
        //        {
        //            return View("Login");
        //        }
        //        model.code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.code));
        //        var result = await userManager.ConfirmEmailAsync(userCheck, model.code);


        //        if (!result.Succeeded)
        //        {
        //            return View("Login");
        //        }
        //        await userManager.SetTwoFactorEnabledAsync(userCheck, true);
        //        TwoFactor mytwoFactor = new TwoFactor()
        //        {
        //            Email = userCheck.Email
        //        };
        //        return RedirectToAction("LoginTwoStep", new { twoFactor = mytwoFactor });


        //    }
        //    else
        //    {
        //        ModelState.AddModelError("message", "Error to set Values.");
        //        return View(model);
        //    }


        //}

        public ActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(string email)
        {

            var user = await userManager.FindByEmailAsync(email);

            var code = await userManager.GenerateChangePhoneNumberTokenAsync(
                               user, user.PhoneNumber);

            SmsService smsHelper = new SmsService();
            smsHelper.SendSmsAsync(user.PhoneNumber, code);

            TwoFactorViewModel twoFactorViewModel = new TwoFactorViewModel()
            {
                Email = email
            };

            return View(twoFactorViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(TwoFactorViewModel twoFactor)
        {
            if (!ModelState.IsValid)
            {
                return View(twoFactor.TwoFactorCode);
            }
            var myuser = await userManager.FindByEmailAsync(twoFactor.Email);
            var result = await userManager.ChangePhoneNumberAsync(myuser, myuser.PhoneNumber, twoFactor.TwoFactorCode);
            if (result.Succeeded)
            {

                if (myuser != null)
                {
                    await signInManager.SignInAsync(myuser, isPersistent: false);
                }
                return RedirectToAction("Index", "Products");
            }
            ModelState.AddModelError("", "Invalid Login Attempt");
            return View();

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);

                if (result.Succeeded)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", user.UserName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));

                    return RedirectToAction("LoginTwoStep", new { email = user.Email });

                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }


        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.Categories = _context.Categories;
            return View();
        }

        // POST api/Account/ChangePassword
       
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await userManager.ChangePasswordAsync(user,
           model.OldPassword, model.NewPassword);


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Errors", error.Description);
                }
                return BadRequest(ModelState);
            }


            await signInManager.RefreshSignInAsync(user);
            ViewBag.Categories = _context.Categories;
            return RedirectToAction("Index","Products");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(model.Email);


            if (user == null)
            {
                return BadRequest();
            }
            var token = await userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultPhoneProvider, "ResetPasswordPurpose");
            
            if (String.IsNullOrEmpty(token))
            {
                return RedirectToAction("ResetPassword");
            }

            SmsService smsHelper = new SmsService();
            smsHelper.SendSmsAsync(user.PhoneNumber, token);

            return RedirectToAction("ResetPassword");

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                return BadRequest();
            }
            var tokenVerified = await userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultPhoneProvider, "ResetPasswordPurpose", model.Code);
            if (!tokenVerified)
                return UnprocessableEntity("invalid user token");

            var token = await userManager.GeneratePasswordResetTokenAsync(user);//new token for reseting password
            var result = await userManager.ResetPasswordAsync(user, token, model.ConfirmPassword);
            

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Errors", error.Description);
                }
                return BadRequest(ModelState);
            }
            return RedirectToAction("Login");
        }

    }
}

