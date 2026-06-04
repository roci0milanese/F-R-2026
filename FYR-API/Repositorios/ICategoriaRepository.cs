using Entidades.Models;
using Datos.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Datos.Interfaces;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> ObtenerTodas();
    Task<Categoria?> ObtenerPorId(int id);
    Task<Categoria> Crear(Categoria categoria);
    Task<bool> Actualizar(Categoria categoria);
    Task<bool> Eliminar(int id);
}