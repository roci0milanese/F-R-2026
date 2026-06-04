using Datos.Interfaces;
using Entidades.Models;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> ObtenerTodas()
    {
        return await _context.Categorias
            .Include(c => c.Subcategorias)
            .Include(c => c.Productos)
            .ToListAsync();
    }

    public async Task<Categoria?> ObtenerPorId(int id)
    {
        return await _context.Categorias
            .Include(c => c.Subcategorias)
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.IdCategoria == id);
    }

    public async Task<Categoria> Crear(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<bool> Actualizar(Categoria categoria)
    {
        var existente = await _context.Categorias.FindAsync(categoria.IdCategoria);
        if (existente == null) return false;

        existente.Nombre = categoria.Nombre;
        existente.EmpresaId = categoria.EmpresaId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Eliminar(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return false;

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return true;
    }
}