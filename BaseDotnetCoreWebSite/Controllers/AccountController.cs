using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseDotnetCoreWebSite.Models;
using BaseDotnetCoreWebSite.Repository;
using BaseDotnetCoreWebSite.Service;
using BaseDotnetCoreWebSite.ViewModels.Account;
using BaseLibrary.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BaseDotnetCoreWebSite.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepository;
        private readonly AccountService _accountService;

        public AccountController(AccountRepository accountRepository, AccountService accountService)
        {
            _accountRepository = accountRepository;
            _accountService = accountService;
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
            if(!ModelState.IsValid)
            {
                return View();
            }

            AccountModel accountModel = _accountRepository.GetAccount(loginViewModel.Account, loginViewModel.Password.ToMD5());

            if(accountModel == null)
            {
                ModelState.AddModelError("", "帳號或密碼錯誤!");
                return View();
            }

            //取得claimsIdentity
            var claimsIdentity = _accountService.GetClaimsIdentity(accountModel);
            //登入使用者
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
            //防止跨網域攻擊
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
