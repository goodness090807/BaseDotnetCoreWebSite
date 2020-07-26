using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseDotnetCoreWebSite.Models;
using BaseDotnetCoreWebSite.Repository;
using BaseDotnetCoreWebSite.ViewModels.Account;
using BaseLibrary.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BaseDotnetCoreWebSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            AccountModel accountModel = _accountRepository.GetAccount(loginViewModel.Account, loginViewModel.Password.ToMD5());

            if(accountModel == null)
            {
                ModelState.AddModelError("", "請輸入正確的帳號或密碼!");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, accountModel.UID),
                new Claim("UserName", accountModel.UserName),
                new Claim(ClaimTypes.Role, "Administrator")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");//導至登入頁
        }
    }
}
