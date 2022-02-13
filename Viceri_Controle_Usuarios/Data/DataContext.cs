using Viceri_Controle_Usuarios.Models;

#pragma warning disable CS1591
namespace Viceri_Controle_Usuarios.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
#pragma warning restore CS1591