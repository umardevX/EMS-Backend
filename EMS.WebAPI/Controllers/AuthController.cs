using Microsoft.AspNetCore.Mvc;
using EMS.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using EMS.Services.Services.Interfaces;
using EMS.WebAPI.Exceptions;

namespace EMS.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) 
        { 
          _authService = authService;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _authService.SignUp(user))
                return Ok();
            else
                throw new BadRequestException("Signup failed.");
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Sigin([FromBody] User user)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _authService.SignIn(user))
                return Ok(_authService.GenerateTokenString(user));
            else
                throw new BadRequestException("Signin failed.");
        }

        [HttpPost("signout")]
        public async Task<IActionResult> Signout()
        {
            if(await _authService.Signout())
                return Ok();      
            else
                throw new BadRequestException("Signout failed.");
        }
    }
}
