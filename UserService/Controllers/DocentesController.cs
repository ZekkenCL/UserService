using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class DocentesController : ControllerBase
{
    private readonly UserDbContext _context;

    public DocentesController(UserDbContext context)
    {
        _context = context;
        InitializeDb();
    }

    private void InitializeDb()
    {
        _context.Database.EnsureCreated();
        if (!_context.Docentes.Any())
        {
            _context.Docentes.AddRange(
                new Docente
                {
                    Nombre = "Juan",
                    Apellido = "Perez",
                    Email = "asdasd@asdad.com",
                    Contraseña = "Juan"
                },
                new Docente
                {
                    Nombre = "Maria",
                    Apellido = "Gomez",
                    Email = "qwe@ewq.cl",
                    Contraseña = "Maria"
                }
            );
            _context.SaveChanges();
        }
    }

    [HttpGet("Docentes")]
    public IActionResult Get()
    {
        var data = _context.Docentes
            .Select(d => new DocenteDTO
            {
                Id = d.Id,
                Nombre = d.Nombre,
                Apellido = d.Apellido,
                Email = d.Email
            })
            .ToList();
        return Ok(new { data });
    }

    [HttpPost("Docentes")]

    public IActionResult Post([FromBody] Docente docente)
    {
        docente.Contraseña = docente.Nombre;
        _context.Docentes.Add(docente);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new {id = docente.Id}, docente);
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] DocenteUpdateDTO docente)
    {
        var docenteEntity = _context.Docentes.FirstOrDefault(d => d.Id == id);
        if (docenteEntity == null)
        {
            return NotFound();
        }

        docenteEntity.Nombre = docente.Nombre;
        docenteEntity.Apellido = docente.Apellido;
        docenteEntity.Email = docente.Email;
        _context.SaveChanges();
        return NoContent();
    }
}