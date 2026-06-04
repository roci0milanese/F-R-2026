using System.ComponentModel.DataAnnotations;

namespace Entidades.DTOs;

public class CrearCategoriaDTO
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public int EmpresaId { get; set; }
}