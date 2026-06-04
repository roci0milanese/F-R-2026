using Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriaController(ICategoriaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categorias = await _service.ObtenerTodas();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var categoria = await _service.ObtenerPorId(id);

        if (categoria == null)
            return NotFound();

        return Ok(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Categoria categoria)
    {
        var nuevaCategoria = await _service.Crear(categoria);

        return CreatedAtAction(
            nameof(Get),
            new { id = nuevaCategoria.IdCategoria },
            nuevaCategoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Categoria categoria)
    {
        var actualizado = await _service.Actualizar(id, categoria);

        if (!actualizado)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _service.Eliminar(id);

        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}