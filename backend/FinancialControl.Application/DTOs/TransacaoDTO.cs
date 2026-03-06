using FinancialControl.Domain.Enums;

namespace FinancialControl.Application.DTOs;

public class TransacaoDTO
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public int CategoriaId { get; set; }
    public int PessoaId { get; set; }
}
