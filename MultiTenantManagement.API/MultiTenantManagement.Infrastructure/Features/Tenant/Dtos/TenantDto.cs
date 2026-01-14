using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantManagement.Infrastructure.Features.Tenant.Dtos
{
    public class TenantDto
    {
        public Guid Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        public string Notice { get; set; }

        public DateTime CreatedAtUtc { get; set; } 

        public DateTime UpdatedAtUtc { get; set; }

    }
}
