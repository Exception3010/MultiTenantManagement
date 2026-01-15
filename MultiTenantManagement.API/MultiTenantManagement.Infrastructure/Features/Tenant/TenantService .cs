
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Data;
using MultiTenantManagement.Infrastructure.Features.Tenant.Dtos;
using MultiTenantManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantManagement.Infrastructure.Features.Tenant
{
    public class TenantService :ITenantService
    {
        private readonly AppDbContext _db;

        public TenantService(AppDbContext db) => _db = db;

        public async Task<List<TenantDto>> GetAllAsync(CancellationToken ct)
        {
            return await _db.Tenants
                .AsNoTracking()
                .OrderByDescending(t => t.CreatedAtUtc)
                .Select(t => new TenantDto() { Id= t.Id ,Status =t.Status,SubDomain =t.SubDomain
                            ,LogoURL=t.LogoURL, Name =t.Name,CreatedAtUtc = t.CreatedAtUtc })
                .ToListAsync(ct);
        }

        public async Task<TenantDto?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.Tenants.AsNoTracking()
                .Where(t => t.Id == id)
                  .Select(t => new TenantDto()
                  {
                      Id = t.Id,
                      Status = t.Status,
                      SubDomain = t.SubDomain,
                      LogoURL = t.LogoURL,
                      Name = t.Name,
                      CreatedAtUtc = t.CreatedAtUtc
                  })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<TenantDto> CreateAsync(CreateTenantRequestDto req, CancellationToken ct)
        {
            var name = req.Name?.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tenant name is required.");

            var tenant = new Data.Models.Tenant
            {
                Name = name,
                CreatedAtUtc = DateTime.UtcNow,
                SubDomain = req.SubDomain,
                Status = req.Status.ToString(),
                LogoURL = req.LogoURL
               
            };

            _db.Tenants.Add(tenant);
            await _db.SaveChangesAsync(ct);

            return new TenantDto() { Id = tenant.Id, Name = tenant.Name, CreatedAtUtc = tenant.CreatedAtUtc };
        }

        public async Task<bool> UpdateAsync(UpdateTenantRequestDto req, CancellationToken ct)
        {
            var name = req.Name?.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tenant name is required.");

            var tenant = await _db.Tenants.FirstOrDefaultAsync(t => t.Id == req.Id, ct);
            if (tenant is null) return false;
            
            tenant.Name = name;
            tenant.SubDomain = req.SubDomain;
            tenant.Status = req.Status.ToString();
            tenant.LogoURL = req.LogoURL;
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
        {
            var tenant = await _db.Tenants.FirstOrDefaultAsync(t => t.Id == id, ct);
            if (tenant is null) return false;

            tenant.IsDeleted = true;
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }

}

