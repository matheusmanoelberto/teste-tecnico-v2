using System;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Enums;
using FinancialControl.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace FinancialControl.Tests.Domain.Entities;

public class TransacaoTests
{
    private readonly Pessoa _pessoaMaiorIdade;
    private readonly Pessoa _pessoaMenorIdade;
    private readonly Categoria _categoriaDespesa;
    private readonly Categoria _categoriaReceita;
    private readonly Categoria _categoriaAmbas;

    public TransacaoTests()
    {
        _pessoaMaiorIdade = new Pessoa("João", 30);
        _pessoaMenorIdade = new Pessoa("Zezinho", 15);
        _categoriaDespesa = new Categoria("Alimentação", FinalidadeCategoria.Despesa);
        _categoriaReceita = new Categoria("Salário", FinalidadeCategoria.Receita);
        _categoriaAmbas = new Categoria("Ajustes", FinalidadeCategoria.Ambas);
    }

    [Fact]
    public void CriarTransacao_ComDadosValidos_DeveCriarComSucesso()
    {
        // Arrange & Act
        var transacao = new Transacao("Compra supermercado", 150.50m, TipoTransacao.Despesa, _categoriaDespesa, _pessoaMaiorIdade);

        // Assert
        transacao.Descricao.Should().Be("Compra supermercado");
        transacao.Valor.Quantia.Should().Be(150.50m);
        transacao.Tipo.Should().Be(TipoTransacao.Despesa);
        transacao.Categoria.Should().Be(_categoriaDespesa);
        transacao.Pessoa.Should().Be(_pessoaMaiorIdade);
    }

    [Fact]
    public void CriarTransacao_ComCategoriaNula_DeveLancarExcecao()
    {
        // Act
        Action action = () => new Transacao("Teste", 100m, TipoTransacao.Despesa, null, _pessoaMaiorIdade);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Categoria é obrigatória.");
    }

    [Fact]
    public void CriarTransacao_ComPessoaNula_DeveLancarExcecao()
    {
        // Act
        Action action = () => new Transacao("Teste", 100m, TipoTransacao.Despesa, _categoriaDespesa, null);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Pessoa é obrigatória.");
    }

    [Fact]
    public void CriarTransacao_ComValorNegativo_DeveLancarExcecao()
    {
        // Act
        Action action = () => new Transacao("Teste", -50m, TipoTransacao.Despesa, _categoriaDespesa, _pessoaMaiorIdade);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("O valor não pode ser negativo.");
    }

    [Fact]
    public void CriarTransacao_ComValorZero_DeveLancarExcecao()
    {
        // Act
        Action action = () => new Transacao("Teste", 0m, TipoTransacao.Despesa, _categoriaDespesa, _pessoaMaiorIdade);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("O valor deve ser maior que zero.");
    }

    [Fact]
    public void CriarTransacao_MenorDeIdadeComReceita_DeveLancarExcecao()
    {
        // Act
        Action action = () => new Transacao("Mesada", 50m, TipoTransacao.Receita, _categoriaReceita, _pessoaMenorIdade);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Menores de 18 anos só podem registrar despesas.");
    }

    [Fact]
    public void CriarTransacao_MenorDeIdadeComDespesa_DevePermitir()
    {
        // Act
        var transacao = new Transacao("Lanche", 20m, TipoTransacao.Despesa, _categoriaDespesa, _pessoaMenorIdade);

        // Assert
        transacao.Valor.Quantia.Should().Be(20m);
        transacao.Tipo.Should().Be(TipoTransacao.Despesa);
    }

    [Fact]
    public void CriarTransacao_ReceitaEmCategoriaExclusivaDeDespesa_DeveLancarExcecao()
    {
        // Act
        Action action = () => new Transacao("Receita indevida", 1000m, TipoTransacao.Receita, _categoriaDespesa, _pessoaMaiorIdade);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Transações do tipo Receita não podem usar categorias exclusivas de Despesa.");
    }

    [Fact]
    public void CriarTransacao_DespesaEmCategoriaExclusivaDeReceita_DeveLancarExcecao()
    {
        // Act
        Action action = () => new Transacao("Despesa indevida", 100m, TipoTransacao.Despesa, _categoriaReceita, _pessoaMaiorIdade);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Transações do tipo Despesa não podem usar categorias exclusivas de Receita.");
    }

    [Fact]
    public void CriarTransacao_CategoriaAmbasParaQualquerTipo_DevePermitir()
    {
        // Act
        var transacaoReceita = new Transacao("Ajuste positivo", 50m, TipoTransacao.Receita, _categoriaAmbas, _pessoaMaiorIdade);
        var transacaoDespesa = new Transacao("Ajuste negativo", 25m, TipoTransacao.Despesa, _categoriaAmbas, _pessoaMaiorIdade);

        // Assert
        transacaoReceita.Tipo.Should().Be(TipoTransacao.Receita);
        transacaoDespesa.Tipo.Should().Be(TipoTransacao.Despesa);
    }
}
