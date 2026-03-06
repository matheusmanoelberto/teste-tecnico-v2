using System.Collections.Generic;
using FinancialControl.Domain.Exceptions;

namespace FinancialControl.Domain.Entities;

public class Pessoa : Entity
{
    public string Nome { get; private set; }
    public int Idade { get; private set; }

    public ICollection<Transacao> Transacoes { get; private set; }

    public Pessoa(string nome, int idade)
    {
        ValidateDomain(nome, idade);
        Nome = nome;
        Idade = idade;
        Transacoes = new List<Transacao>();
    }

    public void Update(string nome, int idade)
    {
        ValidateDomain(nome, idade);
        Nome = nome;
        Idade = idade;
    }

    private void ValidateDomain(string nome, int idade)
    {
        DomainException.When(string.IsNullOrWhiteSpace(nome), "Nome é obrigatório.");
        DomainException.When(nome.Length > 200, "Nome pode ter no máximo 200 caracteres.");
        DomainException.When(idade < 0, "Idade não pode ser negativa.");
    }
}
