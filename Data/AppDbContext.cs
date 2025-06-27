using Microsoft.EntityFrameworkCore;
using WaterJug.Models.Entities;

namespace WaterJug.Data
{
    //
    //
    // DbContext class for db entity management.
    //
    //
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<WaterJugHistory> History { get; set; }

    }
}
