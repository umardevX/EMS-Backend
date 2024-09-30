using EMS.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data
{
    public class EmsDbContext : IdentityDbContext
    {
        public EmsDbContext(DbContextOptions<EmsDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

    }
}
