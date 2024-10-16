﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using e_commerce.Server.Constants;
using e_commerce.Server.DTO.Auth;
using e_commerce.Server.Data;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.Models;
using e_commerce.Server.Data;
using e_commerce.Server.DTO.Response;
using e_commerce.Server.DTO.Accounts;

namespace e_commerce.Server.Services.Services
{
    public class AuthService : IAuthService
    {
        #region Constructor & DI
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogService _logService;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private AuthOptions authOptions;
        #endregion

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogService logService,
            DataContext context, 
            IConfiguration configuration,
            IOptions<AuthOptions> authOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logService = logService;
            _context = context;
            _configuration = configuration;
            this.authOptions = authOptions.Value;
        }

        #region SeedRolesAsync
        public async Task<GeneralServiceResponseDto> SeedRolesAsync()
        {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            bool isManagerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.MANAGER);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);

            if (isOwnerRoleExists && isAdminRoleExists && isManagerRoleExists && isUserRoleExists)
                return new GeneralServiceResponseDto()
                {
                    IsSucceed = true,
                    StatusCode = 200,
                    Message = "Roles Seeding is Already Done"
                };

            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.MANAGER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));

            return new GeneralServiceResponseDto()
            {
                IsSucceed = true,
                StatusCode = 201,
                Message = "Roles Seeding Done Successfully"
            };
        }
        #endregion

        #region Register
        /// <summary>
        /// Feat: Register Account
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var response = new GeneralServiceResponseDto();
            try
            {
                var isExistsUser = await _userManager.FindByNameAsync(registerDto.UserName);
                if (isExistsUser is not null)
                    return new GeneralServiceResponseDto()
                    {
                        IsSucceed = false,
                        StatusCode = 409,
                        Message = "UserName Already Exists"
                    };

                ApplicationUser newUser = new ApplicationUser()
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    UserName = registerDto.UserName,
                    Address = registerDto.Address,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

                if(!createUserResult.Succeeded)
                {
                    var errorString = "User Creation failed because: ";
                    foreach (var error in createUserResult.Errors)
                    {
                        errorString += " # " + error.Description;
                    }
                    return new GeneralServiceResponseDto()
                    {
                        IsSucceed = false,
                        StatusCode = 400,
                        Message = errorString
                    };
                }

                //await _userManager.AddToRoleAsync(newUser, StaticUserRoles.USER);
                await _userManager.AddToRoleAsync(newUser, StaticUserRoles.ADMIN);
                await _logService.SaveNewLog(newUser.UserName, "Registered to Website");
            } 
            catch (Exception ex)
            {
                response.IsSucceed = false;
                response.Message = ex.Message;
            }

            return new GeneralServiceResponseDto()
            {
                IsSucceed = true,
                StatusCode = 201,
                Message = "User Created Successfully"
            };
        }
        #endregion
        #region Login
        /// <summary>
        /// Feat: User login account that registered
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LoginServiceResponseDTO?> LoginAsync(LoginDTO loginDto)
        {
            // Find user with username
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user is null)
                return null;

            // check password of user
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!isPasswordCorrect)
                return null;

            // Return Token and userInfo to front-end
            var newToken = await GenerateJWTTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var userInfo = GenerateUserInfoObject(user,roles);
            await _logService.SaveNewLog(user.UserName, "New Login");

            return new LoginServiceResponseDTO()
            {
                NewToken = newToken,
                UserInfo = userInfo
            };
        }
        #endregion

        #region MeAsync
        public async Task<LoginServiceResponseDTO?> MeAsync(MeDTO meDto)
        {
            ClaimsPrincipal handler = new JwtSecurityTokenHandler().ValidateToken(meDto.Token, new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            }, out SecurityToken securityToken);

            string decodedUserName = handler.Claims.First(q => q.Type == ClaimTypes.Name).Value;
            if (decodedUserName is null)
                return null;

            var user = await _userManager.FindByNameAsync(decodedUserName);
            if (user is null)
                return null;

            var newToken = await GenerateJWTTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var userInfo = GenerateUserInfoObject(user, roles);
            await _logService.SaveNewLog(user.UserName, "New Token Generated");

            return new LoginServiceResponseDTO()
            {
                NewToken = newToken,
                UserInfo = userInfo
            };
        }
        #endregion

        #region GenerateJWTTokenAsync
        private async Task<string> GenerateJWTTokenAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256);

            var tokenObject = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: signingCredentials
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }
        #endregion
        
        #region GenerateUserInfoObject
        private UserInfoResult GenerateUserInfoObject(ApplicationUser user, IEnumerable<string> Roles)
        {
            // Instead of this, You can use Automapper packages. But i don't want it in this project
            return new UserInfoResult()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Roles = Roles
            };
        }
        #endregion

        #region GetUsernamesListAsync
        public async Task<IEnumerable<string>> GetUsernamesListAsync()
        {
            var userNames = await _userManager.Users
                .Select(q => q.UserName)
                .ToListAsync();

            return userNames;
        }
        #endregion
    }
}
