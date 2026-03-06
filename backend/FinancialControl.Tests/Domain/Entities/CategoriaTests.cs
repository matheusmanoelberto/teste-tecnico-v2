using System;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Enums;
using FinancialControl.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace FinancialControl.Tests.Domain.Entities;

public class CategoriaTests
{
    [Fact]
    public void CriarCategoria_ComDadosValidos_DeveCriarComSucesso()
    {
        // Arrange & Act
        var categoria = new Categoria("Alimentação", FinalidadeCategoria.Despesa);

        // Assert
        categoria.Descricao.Should().Be("Alimentação");
        categoria.Finalidade.Should().Be(FinalidadeCategoria.Despesa);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarCategoria_ComDescricaoVazia_DeveLancarExcecao(string? descricaoInvalida)
    {
        // Arrange & Act
        Action action = () => new Categoria(descricaoInvalida, FinalidadeCategoria.Receita);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Descrição é obrigatória.");
    }

    [Fact]
    public void CriarCategoria_ComDescricaoMaiorQue400Caracteres_DeveLancarExcecao()
    {
        // Arrange
        var descricaoInvalida = new string('A', 401);

        // Act
        Action action = () => new Categoria(descricaoInvalida, FinalidadeCategoria.Ambas);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Descrição pode ter no máximo 400 caracteres.");
    }
}
