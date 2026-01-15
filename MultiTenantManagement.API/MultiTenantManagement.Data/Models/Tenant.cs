using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantManagement.Data.Models
{
    public  class Tenant
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(200)]
        public string Name { get; set; } = default!;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; }

        [Required]
        public string Status { get; set; }

        public string? LogoURL { get; set; }

        [Required, MaxLength(250)]
        public string SubDomain { get; set; } 

        // Navigation
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();


    }
}
