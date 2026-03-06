using System;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace FinancialControl.Tests.Domain.Entities;

public class PessoaTests
{
    [Fact]
    public void CriarPessoa_ComDadosValidos_DeveCriarComSucesso()
    {
        // Arrange & Act
        var pessoa = new Pessoa("Matheus", 25);

        // Assert
        pessoa.Nome.Should().Be("Matheus");
        pessoa.Idade.Should().Be(25);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarPessoa_ComNomeVazioOuNulo_DeveLancarExcecao(string? nomeInvalido)
    {
        // Arrange & Act
        Action action = () => new Pessoa(nomeInvalido, 25);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Nome é obrigatório.");
    }

    [Fact]
    public void CriarPessoa_ComNomeMaiorQue200Caracteres_DeveLancarExcecao()
    {
        // Arrange
        var nomeInvalido = new string('A', 201);

        // Act
        Action action = () => new Pessoa(nomeInvalido, 25);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Nome pode ter no máximo 200 caracteres.");
    }

    [Fact]
    public void CriarPessoa_ComIdadeNegativa_DeveLancarExcecao()
    {
        // Arrange & Act
        Action action = () => new Pessoa("Lucas", -1);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Idade não pode ser negativa.");
    }

    [Fact]
    public void CriarPessoa_ComIdadeZero_DeveSerPermitido()
    {
        // Arrange & Act
        var pessoa = new Pessoa("Lucas", 0);

        // Assert
        pessoa.Idade.Should().Be(0);
        pessoa.Nome.Should().Be("Lucas");
    }
}
