using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class EstudiantesController : ControllerBase
{
    private readonly UserDbContext _context;

    public EstudiantesController(UserDbContext context)
    {
        _context = context;
        InitializeDb();
    }

    private void InitializeDb()
    {
        _context.Database.EnsureCreated();
        if (!_context.Estudiantes.Any())
        {
            _context.Estudiantes.AddRange(
                new Estudiante
                {
                    Nombre = "Juan",
                    Apellido = "Perez",
                    Email = "asdasd@asdad.com",
                },
                new Estudiante
                {
                    Nombre = "Maria",
                    Apellido = "Gomez",
                    Email = "qwe@ewq.cl",
                }
            );
            _context.SaveChanges();
        }
    }

    [HttpGet("Estudiantes")]
    public IActionResult Get()
    {
        var data = _context.Estudiantes.ToList();
        return Ok(new {data});
    }

    [HttpPost("Estudiantes")]
    public IActionResult Post([FromBody] Estudiante estudiante)
    {
        _context.Estudiantes.Add(estudiante);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new {id = estudiante.Id}, estudiante);
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Estudiante estudiante)
    {
        var estudianteEntity = _context.Estudiantes.FirstOrDefault(e => e.Id == id);
        if (estudianteEntity == null)
        {
            return NotFound();
        }

        estudianteEntity.Nombre = estudiante.Nombre;
        estudianteEntity.Apellido = estudiante.Apellido;
        estudianteEntity.Email = estudiante.Email;
        _context.SaveChanges();
        return NoContent();
    }
}