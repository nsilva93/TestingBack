using Microsoft.EntityFrameworkCore;
using TestingBack.CORE.Models.NombreProyecto;
using TestingBack.SERVICE.DTO.NombreProyecto;

namespace TestingBack.INFRASTRUCTURE.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategorysDTO>().HasNoKey().ToView(null);



        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductSubcategory> ProductSubcategory { get; set; }

        public DbSet<CategorysDTO> CategorysDTO { get; set; }
    }
}
