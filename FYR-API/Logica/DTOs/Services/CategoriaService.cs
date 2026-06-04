using Datos.Interfaces;
using Entidades.Models;
using Servicios.Interfaces;

namespace Servicios;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;

    public CategoriaService(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Categoria>> ObtenerTodas()
    {
        return await _repository.ObtenerTodas();
    }

    public async Task<Categoria?> ObtenerPorId(int id)
    {
        return await _repository.ObtenerPorId(id);
    }

    public async Task<Categoria> Crear(Categoria categoria)
    {
        return await _repository.Crear(categoria);
    }

    public async Task<bool> Actualizar(int id, Categoria categoria)
    {
        var categoriaExistente = await _repository.ObtenerPorId(id);

        if (categoriaExistente == null)
            return false;

        categoriaExistente.Nombre = categoria.Nombre;
        categoriaExistente.EmpresaId = categoria.EmpresaId;

        await _repository.Actualizar(categoriaExistente);

        return true;
    }

    public async Task<bool> Eliminar(int id)
    {
        var categoria = await _repository.ObtenerPorId(id);

        if (categoria == null)
            return false;

        await _repository.Eliminar(id);

        return true;
    }
}