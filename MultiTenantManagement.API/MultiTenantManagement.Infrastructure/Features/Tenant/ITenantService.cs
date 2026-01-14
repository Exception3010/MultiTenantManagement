using MultiTenantManagement.Infrastructure.Features.Tenant.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantManagement.Infrastructure.Features.Tenant
{
    public interface ITenantService
    {
        Task<List<TenantDto>> GetAllAsync(CancellationToken ct);
        Task<TenantDto?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<TenantDto> CreateAsync(CreateTenantRequestDto req, CancellationToken ct);
        Task<bool> UpdateAsync(UpdateTenantRequestDto req, CancellationToken ct);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct);
    }
}
