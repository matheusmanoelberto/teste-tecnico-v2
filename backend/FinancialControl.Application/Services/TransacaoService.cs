using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialControl.Application.DTOs;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Enums;
using FinancialControl.Domain.Exceptions;
using FinancialControl.Domain.Repositories;

namespace FinancialControl.Application.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public TransacaoService(
        ITransacaoRepository transacaoRepository,
        IPessoaRepository pessoaRepository,
        ICategoriaRepository categoriaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _pessoaRepository = pessoaRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<TransacaoDTO>> GetAllAsync()
    {
        var transacoes = await _transacaoRepository.GetAllAsync();
        return transacoes.Select(t => new TransacaoDTO
        {
            Id = t.Id,
            Descricao = t.Descricao,
            Valor = t.Valor,
            Tipo = t.Tipo,
            CategoriaId = t.CategoriaId,
            PessoaId = t.PessoaId
        });
    }

    public async Task<TransacaoDTO> AddAsync(TransacaoDTO transacaoDto)
    {
        var pessoa = await _pessoaRepository.GetByIdAsync(transacaoDto.PessoaId);
        DomainException.When(pessoa == null, "Pessoa não encontrada.");

        var categoria = await _categoriaRepository.GetByIdAsync(transacaoDto.CategoriaId);
        DomainException.When(categoria == null, "Categoria não encontrada.");

        var transacao = new Transacao(
            transacaoDto.Descricao,
            transacaoDto.Valor,
            transacaoDto.Tipo,
            categoria,
            pessoa);

        var created = await _transacaoRepository.AddAsync(transacao);
        transacaoDto.Id = created.Id;

        return transacaoDto;
    }

    public async Task DeleteAsync(int id)
    {
        var transacao = await _transacaoRepository.GetByIdAsync(id);
        DomainException.When(transacao == null, "Transação não encontrada.");
        await _transacaoRepository.DeleteAsync(id);
    }
}
