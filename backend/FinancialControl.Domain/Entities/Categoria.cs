using FinancialControl.Domain.Enums;
using FinancialControl.Domain.Exceptions;

namespace FinancialControl.Domain.Entities;

public class Categoria : Entity
{
    public string Descricao { get; private set; }
    public FinalidadeCategoria Finalidade { get; private set; }

    public ICollection<Transacao> Transacoes { get; private set; }

    public Categoria(string descricao, FinalidadeCategoria finalidade)
    {
        ValidateDomain(descricao, finalidade);
        Descricao = descricao;
        Finalidade = finalidade;
        Transacoes = new List<Transacao>();
    }

    public void Update(string descricao, FinalidadeCategoria finalidade)
    {
        ValidateDomain(descricao, finalidade);
        Descricao = descricao;
        Finalidade = finalidade;
    }

    private void ValidateDomain(string descricao, FinalidadeCategoria finalidade)
    {
        DomainException.When(string.IsNullOrWhiteSpace(descricao), "Descrição é obrigatória.");
        DomainException.When(descricao.Length > 400, "Descrição pode ter no máximo 400 caracteres.");
    }
}
