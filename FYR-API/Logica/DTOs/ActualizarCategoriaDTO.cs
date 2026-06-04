using System.ComponentModel.DataAnnotations;

namespace Entidades.DTOs;

public class ActualizarCategoriaDTO
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public int EmpresaId { get; set; }
}