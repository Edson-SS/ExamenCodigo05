using Microsoft.EntityFrameworkCore;

namespace ExamenCodigo.Models
{
    public class DemoContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MSI\\EQUIPO; " +
                "Database=CodigoExamen05; Integrated Security=True;" +
                "Trust Server Certificate=True ");
        }
    }
}
