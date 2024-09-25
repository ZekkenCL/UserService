using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly UserDbContext _context;

    public UsuariosController(UserDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAllUsers()
    {
        var docentes = await _context.Docentes
            .Select(docente => new UsuarioDTO
            {
                Id = docente.Id,
                Nombre = docente.Nombre,
                Apellido = docente.Apellido,
                Email = docente.Email,
                Tipo = "Docente"
            })
            .ToListAsync();


        var estudiantes = await _context.Estudiantes
            .Select(estudiante => new UsuarioDTO
            {
                Id = estudiante.Id,
                Nombre = estudiante.Nombre,
                Apellido = estudiante.Apellido,
                Email = estudiante.Email,
                Tipo = "Estudiante"
            })
            .ToListAsync();


        var usuarios = docentes.Concat(estudiantes).ToList();

        return usuarios;
    }
}
