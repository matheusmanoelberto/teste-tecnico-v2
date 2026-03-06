using FinancialControl.Domain.Enums;

namespace FinancialControl.Application.DTOs;

public class CategoriaDTO
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public FinalidadeCategoria Finalidade { get; set; }
}
