using FinancialControl.Domain.Enums;
using FinancialControl.Domain.Exceptions;
using FinancialControl.Domain.ValueObjects;

namespace FinancialControl.Domain.Entities;

public class Transacao : Entity
{
    public string Descricao { get; private set; }
    public Dinheiro Valor { get; private set; }
    public TipoTransacao Tipo { get; private set; }

    public int CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; }

    public int PessoaId { get; private set; }
    public Pessoa Pessoa { get; private set; }

    protected Transacao() { } // Construtor para o EF Core

    public Transacao(string descricao, decimal valor, TipoTransacao tipo, Categoria categoria, Pessoa pessoa)
    {
        DomainException.When(categoria == null, "Categoria é obrigatória.");
        DomainException.When(pessoa == null, "Pessoa é obrigatória.");

        ValidateDomain(descricao);
        var valorVO = new Dinheiro(valor);
        DomainException.When(valorVO.Quantia <= 0, "O valor deve ser maior que zero."); // Regra adicional no nível da transação

        // Regra 1: Menor de idade
        if (pessoa.Idade < 18)
        {
            DomainException.When(tipo == TipoTransacao.Receita, "Menores de 18 anos só podem registrar despesas.");
        }

        // Regra 2: Compatibilidade de Categoria
        if (tipo == TipoTransacao.Receita)
        {
            DomainException.When(categoria.Finalidade == FinalidadeCategoria.Despesa, "Transações do tipo Receita não podem usar categorias exclusivas de Despesa.");
        }
        else if (tipo == TipoTransacao.Despesa)
        {
            DomainException.When(categoria.Finalidade == FinalidadeCategoria.Receita, "Transações do tipo Despesa não podem usar categorias exclusivas de Receita.");
        }

        Descricao = descricao;
        Valor = valorVO;
        Tipo = tipo;
        CategoriaId = categoria.Id;
        Categoria = categoria;
        PessoaId = pessoa.Id;
        Pessoa = pessoa;
    }

    private void ValidateDomain(string descricao)
    {
        DomainException.When(string.IsNullOrWhiteSpace(descricao), "Descrição é obrigatória.");
        DomainException.When(descricao.Length > 400, "Descrição pode ter no máximo 400 caracteres.");
    }
}
