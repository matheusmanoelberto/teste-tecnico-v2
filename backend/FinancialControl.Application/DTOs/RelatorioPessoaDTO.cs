namespace FinancialControl.Application.DTOs;

public class RelatorioPessoaDTO
{
    public string NomePessoa { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}
