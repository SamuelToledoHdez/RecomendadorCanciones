using Microsoft.EntityFrameworkCore;
using RecomendadorCanciones.Models;

namespace RecomendadorCanciones.Data;
  // Cambia esto por el namespace correcto

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Song> Songs { get; set; }
}