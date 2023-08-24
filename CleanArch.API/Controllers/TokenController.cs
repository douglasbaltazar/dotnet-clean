using CleanArch.API.Models;
using CleanArch.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TokenController : ControllerBase
	{
		private readonly IAuthenticate _authentication;
		private readonly IConfiguration _configuration;
		public TokenController(IAuthenticate authentication, IConfiguration configuration)
		{
			_authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		[HttpPost("LoginUser")]
		public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
		{
			var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);
			if (result)
			{
				return GenerateToken(userInfo);
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid Login attemp");
				return BadRequest(ModelState);
			}
		}
		
		[HttpPost("CreateUser")]
		public async Task<ActionResult> CreateUser([FromBody] LoginModel registerModel)
		{
			var result = await _authentication.RegisterUser(registerModel.Email, registerModel.Password);
			if (result)
			{
				return Ok($"User {registerModel.Email} was created");
			} else
			{
				ModelState.AddModelError(string.Empty, "Invalid Login attemp");
				return BadRequest(ModelState);
			}
		}

		private UserToken GenerateToken(LoginModel userInfo)
		{
			var claims = new[]
			{
				new Claim("email", userInfo.Email),
				new Claim("meuvalor", "o que cv quiser"),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			// gerar chave privada para assinar o token
			var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

			// gerar assinatura digital
			var crediantials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

			// deifnir tempo de expiração
			var expiration = DateTime.UtcNow.AddMinutes(10);

			JwtSecurityToken token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: expiration,
				signingCredentials: crediantials
			);

			return new UserToken()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Expiration = expiration
			};
		}
	}
}
