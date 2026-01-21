using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantManagement.Infrastructure.Features.Authentication;
using MultiTenantManagement.Infrastructure.Features.Authentication.Dtos;

namespace MultiTenantManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await authenticationService.LoginAsync(dto);
            return Ok(result);
        }
    }
}
