using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Trello.Server.Core;

namespace Trello.Server.Service;

public class AuthService : IAuthService {

	private readonly SignInManager<User> _signInManager;
	private readonly UserManager<User> _userManager;
	private readonly IConfiguration _configuration;

	public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration) {
		_userManager = userManager;
		_signInManager = signInManager;
		_configuration = configuration;
	}

	public async Task<LoginResponse> Login(string email, string password) {
		try {
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null) {
				throw new Exception("Invalid email or password");
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
			if (!result.Succeeded) {
				throw new Exception("Invalid email or password");
			}

			var token = await GenerateJwtToken(user);

			return new LoginResponse {
				User = user,
				Token = token
			};

		}
		catch (Exception ex) {
			throw new Exception("Error on login", ex);
		}
	}

	public async Task Register(string firstname, string lastname, string email, string password) {
		try {
				var result = await _userManager.CreateAsync(new User {
						 Firstname = firstname,
						 Lastname = lastname,
						 Email = email,
						 UserName = email,
						 CreatedAt = DateTime.UtcNow,
						 UpdatedAt = DateTime.UtcNow,
			   }, password);

			   if (result.Errors.Count() > 0) {
						throw new Exception(result.Errors.First().Description);
			   }

		}
		catch (Exception ex) {
			throw new Exception(ex.Message, ex);
		}
	}

	public Task Logout() {
		throw new NotImplementedException();
	}

	private async Task<string> GenerateJwtToken(User user) {
		var claims = new List<Claim> {
			new Claim(ClaimTypes.Name, user.Fullname),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim("CreatedAt", user.CreatedAt.ToString()),
			new Claim("UpdatedAt", user.UpdatedAt.ToString()),
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _configuration["JWT:Issuer"],
			audience: _configuration["JWT:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble((_configuration["Jwt:ExpiresInMinutes"]))),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}