﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Server.DTO.Accounts;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.DTO.Auth;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _accountService;
        public AuthController(IAuthService accountService)
        {
            _accountService = accountService;
        }

        // Route -> Seed Roles to DB
        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var seedResult = await _accountService.SeedRolesAsync();
            return StatusCode(seedResult.StatusCode, seedResult.Message);
        }

        #region Register
        [HttpPost()]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var registerResult = await _accountService.RegisterAsync(registerDto);
            return StatusCode(registerResult.StatusCode, registerResult.Message);
        }
        #endregion

        #region Login
        [HttpPost(), AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<LoginServiceResponseDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginResult = await _accountService.LoginAsync(loginDTO);
            if (loginResult is null)
            {
                return Unauthorized("Your credentials are invalid. Please contact to an Admin");
            }

            return Ok(loginResult);
        }
        #endregion

        #region Me
        [HttpPost]
        [Route("me")]
        public async Task<ActionResult<LoginServiceResponseDTO>> Me([FromBody] MeDTO token)
        {
            try
            {
                var me = await _accountService.MeAsync(token);
                if(me is not null)
                {
                    return Ok(me);
                }
                else
                {
                    return Unauthorized("Invalid Token");
                }
            }
            catch (Exception)
            {
                return Unauthorized("Invalid Token");
            }
        }
        #endregion

        #region GetUserNamesList
        [HttpGet]
        [Route("usernames")]
        public async Task<ActionResult<IEnumerable<string>>> GetUserNamesList()
        {
            var usernames = await _accountService.GetUsernamesListAsync();

            return Ok(usernames);
        }
        #endregion
        //[Authorize]
        //[HttpPost("SignOut")]
        //public IActionResult SignOut()
        //{
        //    string id = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
        //    if (String.IsNullOrEmpty(id)) return NotFound();
        //    _accountService.SignOut(Int32.Parse(id));
        //    return Ok();
        //}
    }
}
