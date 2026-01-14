using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantManagement.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        public bool IsDeleted { get; set; }
    }
}
