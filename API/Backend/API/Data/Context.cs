using APIFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace APIFinal.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        
    }

    public DbSet<Usuario> Usuarios {get; set;}
    public DbSet<Avion> Aviones {get; set;}
    
}
