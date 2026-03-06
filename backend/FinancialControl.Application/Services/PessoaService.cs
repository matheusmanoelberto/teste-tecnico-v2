using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialControl.Application.DTOs;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Exceptions;
using FinancialControl.Domain.Repositories;

namespace FinancialControl.Application.Services;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _repository;

    public PessoaService(IPessoaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PessoaDTO>> GetAllAsync()
    {
        var pessoas = await _repository.GetAllAsync();
        return pessoas.Select(p => new PessoaDTO
        {
            Id = p.Id,
            Nome = p.Nome,
            Idade = p.Idade
        });
    }

    public async Task<PessoaDTO> GetByIdAsync(int id)
    {
        var pessoa = await _repository.GetByIdAsync(id);
        if (pessoa == null) return null;

        return new PessoaDTO { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade };
    }

    public async Task<PessoaDTO> AddAsync(PessoaDTO pessoaDto)
    {
        var pessoa = new Pessoa(pessoaDto.Nome, pessoaDto.Idade);
        var created = await _repository.AddAsync(pessoa);

        pessoaDto.Id = created.Id;
        return pessoaDto;
    }

    public async Task UpdateAsync(int id, PessoaDTO pessoaDto)
    {
        var pessoa = await _repository.GetByIdAsync(id);
        DomainException.When(pessoa == null, "Pessoa não encontrada.");

        pessoa.Update(pessoaDto.Nome, pessoaDto.Idade);
        _repository.Update(pessoa);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var pessoa = await _repository.GetByIdAsync(id);
        DomainException.When(pessoa == null, "Pessoa não encontrada.");

        _repository.Delete(pessoa!);
        await _repository.SaveChangesAsync();
    }
}
