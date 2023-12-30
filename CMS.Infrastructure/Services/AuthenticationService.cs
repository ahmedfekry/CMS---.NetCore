using CMS.Core.Services;
using CMS.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMS.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        public AuthenticationService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }
        public async Task<AuthModel> RegisterUserAsync(RegisterModel registerModel)
        {
            try
            {

                if (registerModel == null)
                    throw new ArgumentNullException(nameof(registerModel));

                if (await _userManager.FindByEmailAsync(registerModel.Email) is not null)
                {
                    return new AuthModel { Message = "Email Already Exists" };
                }

                if (await _userManager.FindByNameAsync(registerModel.Username) is not null)
                {
                    return new AuthModel { Message = "Username already exists" };
                }

                var user = new ApplicationUser
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName
                };

                var result = await _userManager.CreateAsync(user,registerModel.Password);

                if (!result.Succeeded)
                {
                    string error = string.Empty;

                    foreach (var item in result.Errors)
                    {
                        error += $"{item.Description},";
                    }

                    return new AuthModel { Message = error };
                }

                await _userManager.AddToRolesAsync(user, registerModel.Roles);
                var jwtSecToken = await GenerateJWTToken(user);

                var AuthRes = new AuthModel
                {
                    Email = user.Email,
                    ExpireOn = jwtSecToken.ValidTo,
                    IsAuthenticated = true,
                    Message = "User Authenticated successfuly",
                    Username = user.UserName,
                    Roles = registerModel.Roles,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecToken)
                };

                return AuthRes;
            }
            catch (Exception ex)
            {
                return new AuthModel { Message = "Registeration failed with exception => " + ex.ToString() };
            }

        }

        public async Task<JwtSecurityToken> GenerateJWTToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid",user.Id)
            }.Union(userClaims)
            .Union(roleClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecKey = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(double.Parse(_jwt.DurationInDays)),
                    signingCredentials: signingCredentials
                );

            return jwtSecKey;
        }

        public async Task<AuthModel> LoginUserAsync(LoginModel loginModel)
        {
            AuthModel authModel = new AuthModel();

            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(loginModel.Email);
            if (applicationUser is null)
            {
                authModel.Message = "Email or password is incorrect";
                return authModel;
            }

            if(!await _userManager.CheckPasswordAsync(applicationUser, loginModel.Password))
            {
                authModel.Message = "Email or password is incorrect";
                return authModel;
            }

            var jwtSecToken = await GenerateJWTToken(applicationUser);

            authModel = new AuthModel
            {
                Email = applicationUser.Email,
                ExpireOn = jwtSecToken.ValidTo,
                IsAuthenticated = true,
                Message = "User Authenticated successfuly",
                Username = applicationUser.UserName,
                Roles = (List<string>) await _userManager.GetRolesAsync(applicationUser),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecToken)
            };

            return authModel;
        }
    }
}
