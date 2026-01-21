using Microsoft.AspNetCore.Mvc;
using MultiTenantManagement.Infrastructure.Features.Tenant.Dtos;
using MultiTenantManagement.Infrastructure.Features.Tenant;

namespace MultiTenantManagement.API.Controllers
{
    [Route("Tenants")]
    public class TenantsController(ITenantService tenantService) : ControllerBase
    {
        [HttpGet, Route("GetTenants")]
        public async Task<ActionResult<List<TenantDto>>> GetAll(CancellationToken ct)
            => Ok(await tenantService.GetAllAsync(ct));

        [HttpGet, Route("Tenant/{id}")]
        public async Task<ActionResult<TenantDto>> GetById([FromRoute]Guid id, CancellationToken ct)
        {
            var tenant = await tenantService.GetByIdAsync(id, ct);
            return tenant is null ? NotFound() : Ok(tenant);
        }

        [HttpPost, Route("CreateTenant")]
        public async Task<ActionResult<TenantDto>> Create([FromBody] CreateTenantRequestDto req, CancellationToken ct)
            => Ok(await tenantService.CreateAsync(req, ct));

        [HttpPut, Route("UpdateTenant")]
        public async Task<IActionResult> Update([FromBody] UpdateTenantRequestDto req, CancellationToken ct)
        {
            var ok = await tenantService.UpdateAsync( req, ct);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete, Route("Tenant/{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var ok = await tenantService.DeleteAsync(id, ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
