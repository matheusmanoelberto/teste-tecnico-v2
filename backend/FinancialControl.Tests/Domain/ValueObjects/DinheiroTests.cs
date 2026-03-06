using System;
using FinancialControl.Domain.Exceptions;
using FinancialControl.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace FinancialControl.Tests.Domain.ValueObjects;

public class DinheiroTests
{
    [Fact]
    public void CriarDinheiro_ComValorValido_DeveCriarComSucesso()
    {
        // Act
        var dinheiro = new Dinheiro(150.50m);

        // Assert
        dinheiro.Quantia.Should().Be(150.50m);
    }

    [Fact]
    public void CriarDinheiro_ComValorNegativo_DeveLancarExcecao()
    {
        // Act
        Action action = () => new Dinheiro(-10m);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("O valor não pode ser negativo.");
    }

    [Fact]
    public void Dinheiro_ComparacaoDeValoresIguais_DeveSerVerdadeiro()
    {
        // Arrange
        var d1 = new Dinheiro(100m);
        var d2 = new Dinheiro(100m);

        // Act & Assert
        d1.Should().Be(d2);
    }

    [Fact]
    public void Dinheiro_ComparacaoDeValoresDiferentes_DeveSerFalso()
    {
        // Arrange
        var d1 = new Dinheiro(100m);
        var d2 = new Dinheiro(200m);

        // Act & Assert
        d1.Should().NotBe(d2);
    }
}
