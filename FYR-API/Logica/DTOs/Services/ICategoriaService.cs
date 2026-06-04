using Entidades.Models;

namespace Servicios.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> ObtenerTodas();

    Task<Categoria?> ObtenerPorId(int id);

    Task<Categoria> Crear(Categoria categoria);

    Task<bool> Actualizar(int id, Categoria categoria);

    Task<bool> Eliminar(int id);
}