using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantManagement.Infrastructure.Features.Users;
using MultiTenantManagement.Infrastructure.Features.Users.Dtos;
using MultiTenantManagement.Infrastructure.Helpers;

namespace MultiTenantManagement.API.Controllers
{
    [Route("Users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController(IUserManagementService users) : ControllerBase
    {
        [HttpGet]
        [Route("GetUsers")]
        [Authorize(Roles = "SystemAdmin,TenantAdmin")]
        public async Task<IActionResult> GetUsers([FromQuery] Guid? tenantId, CancellationToken ct = default)
        {
            if (User.IsTenantAdmin())
            {
                var myTenant = User.GetTenantId();
                if (myTenant is null) return Forbid();
                tenantId = myTenant;
            }

            return Ok(await users.GetUsersAsync(tenantId,ct));
        }

        [HttpGet]
        [Route("Users/{userId}")]
        [Authorize(Roles = "SystemAdmin,TenantAdmin")]
        public async Task<IActionResult> GetById(string userId, CancellationToken ct)
        {
            var u = await users.GetByIdAsync(userId, ct);
            if (u is null) return NotFound();

            if (User.IsTenantAdmin())
            {
                var myTenant = User.GetTenantId();
                if (myTenant is null) return Forbid();
                if (u.TenantId != myTenant) return Forbid();
            }

            return Ok(u);
        }

        [HttpPost]
        [Route("CreateUser")]
        [Authorize(Roles = "SystemAdmin,TenantAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto, CancellationToken ct)
        {
            var actorId = User.GetUserId();
            if (string.IsNullOrEmpty(actorId)) return Unauthorized();

            if (User.IsTenantAdmin())
            {
                var myTenant = User.GetTenantId();
                if (myTenant is null) return Forbid();

                if (string.Equals(dto.Role, "SystemAdmin", StringComparison.OrdinalIgnoreCase))
                    return Forbid();

                dto.TenantId = myTenant;
            }

            var created = await users.CreateUserAsync(dto, actorId, ct);
            return Ok(created);
        }

        [HttpPut]
        [Route("UpdateUser/{userId}")]
        [Authorize(Roles = "SystemAdmin,TenantAdmin")]
        public async Task<IActionResult> Update(string userId, [FromBody] UpdateUserDto dto, CancellationToken ct)
        {
            var actorId = User.GetUserId();
            if (string.IsNullOrEmpty(actorId)) return Unauthorized();

            if (User.IsTenantAdmin())
            {
                var myTenant = User.GetTenantId();
                if (myTenant is null) return Forbid();

                var target = await users.GetByIdAsync(userId, ct);
                if (target is null) return NotFound();
                if (target.TenantId != myTenant) return Forbid();

                dto.TenantId = myTenant; 
                if (string.Equals(dto.Role, "SystemAdmin", StringComparison.OrdinalIgnoreCase))
                    return Forbid();
            }

            var ok = await users.UpdateUserAsync(userId, dto, actorId, ct);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        [Authorize(Roles = "SystemAdmin,TenantAdmin")]
        public async Task<IActionResult> Delete(string userId, CancellationToken ct)
        {
            var actorId = User.GetUserId();
            if (string.IsNullOrEmpty(actorId)) return Unauthorized();

            if (User.IsTenantAdmin())
            {
                var myTenant = User.GetTenantId();
                if (myTenant is null) return Forbid();

                var target = await users.GetByIdAsync(userId, ct);
                if (target is null) return NotFound();
                if (target.TenantId != myTenant) return Forbid();
            }

            var ok = await users.SoftDeleteAsync(userId, actorId, ct);
            return ok ? NoContent() : NotFound();
        }

    }
}
