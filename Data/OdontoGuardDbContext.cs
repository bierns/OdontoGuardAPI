using Microsoft.EntityFrameworkCore;
using OdontoGuardAPI.Models;

namespace OdontoGuardAPI.Data
{
    public class OdontoGuardDbContext : DbContext
    {
        public OdontoGuardDbContext(DbContextOptions<OdontoGuardDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
