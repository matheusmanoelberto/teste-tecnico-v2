using FinancialControl.Domain.Exceptions;
using System.Collections.Generic;

namespace FinancialControl.Domain.ValueObjects;

public class Dinheiro : ValueObject
{
    public decimal Quantia { get; private set; }

    protected Dinheiro() { }

    public Dinheiro(decimal quantia)
    {
        DomainException.When(quantia < 0, "O valor não pode ser negativo.");
        Quantia = quantia;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Quantia;
    }

    public static implicit operator decimal(Dinheiro dinheiro) => dinheiro.Quantia;
    public static implicit operator Dinheiro(decimal quantia) => new Dinheiro(quantia);
}
