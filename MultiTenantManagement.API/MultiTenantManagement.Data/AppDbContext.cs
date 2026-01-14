using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantManagement.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }


        public DbSet<Tenant> Tenants => Set<Tenant>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
                base.OnModelCreating(builder);




                builder.Entity<Tenant>(e =>
                {
                    e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                });

                builder.Entity<ApplicationUser>(e =>
                {
                    e.HasIndex(x => x.TenantId);

                    e.HasOne(u => u.Tenant)
                     .WithMany(t => t.Users)
                     .HasForeignKey(u => u.TenantId)
                     .OnDelete(DeleteBehavior.Restrict)
                     .IsRequired(false);
                });
        }

    }
}