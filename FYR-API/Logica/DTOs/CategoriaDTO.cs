namespace Entidades.DTOs;

public class CategoriaDTO
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public int EmpresaId { get; set; }
}