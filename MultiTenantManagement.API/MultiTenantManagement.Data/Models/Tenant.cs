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

        public DateTime UpdatedAtUtc { get; set; }

        public string? Notice { get; set; }

        public bool IsDeleted { get; set; }


        // Navigation
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();


    }
}
