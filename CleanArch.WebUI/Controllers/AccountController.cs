using CleanArch.Domain.Account;
using CleanArch.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAuthenticate _authentication;
		public AccountController(IAuthenticate authentication)
		{
			_authentication = authentication;
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model) {
			var result = await _authentication.RegisterUser(model.Email, model.Password);
			if (result)
			{
				return Json(result);
			} else
			{
				return Json("Invalid register attempt (password must be strong).");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model) {
			var result = await _authentication.Authenticate(model.Email, model.Password);
			if(result)
			{
				return Json(result);
			} else
			{
				return Json("Invalid login attempt. (password must be strong).");
			}
		}

		public async Task<IActionResult> Logout() {
			await _authentication.Logout();
			return Json("Logout ok");
		}
	}
}
