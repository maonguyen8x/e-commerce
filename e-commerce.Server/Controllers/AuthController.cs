using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Server.DTO.Accounts;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.DTO.Auth;
using e_commerce.Server.Constants;

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

        [HttpPost()]
        [Route("register-admin")]
        [Authorize(Roles = StaticUserRoles.ADMIN)]
        public async Task<IActionResult> RegisterAdmin(RegisterDto model)
        {
            var result = await _accountService.RegisterAdminAsync(model);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
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
            
            var registerResult = await _accountService.RegisterUserAsync(registerDto);
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
                return BadRequest(new { message = "Invalid data", status = 400 });
            }

            try
            {
                // Call to LoginAsync && receive the response
                var loginResult = await _accountService.LoginAsync(loginDTO);

                if (!loginResult.IsSucceed)
                {
                    return StatusCode(loginResult.StatusCode, new { message = loginResult.Message, status = loginResult.StatusCode });
                }

                // IsSucceed, return token, userInfo && messages
                return Ok(new
                {
                    message = loginResult.Message,
                    status = loginResult.StatusCode,
                    token = ((LoginServiceResponseDTO)loginResult.Data).NewToken,
                    userInfo = ((LoginServiceResponseDTO)loginResult.Data).UserInfo
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", error = ex.Message });
            }
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
        public async Task<ActionResult<IEnumerable<string>>> GetByUserName(string username)
        {
            var usernames = await _accountService.GetByUsernameAsync(username);
            if (usernames == null || !usernames.Any()) // Check for null or empty collection
            {
                return NotFound("No usernames found.");
            }

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
