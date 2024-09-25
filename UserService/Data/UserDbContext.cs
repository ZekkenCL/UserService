using Microsoft.EntityFrameworkCore;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<Docente> Docentes { get; set; }
    public DbSet<Estudiante> Estudiantes { get; set; }
}