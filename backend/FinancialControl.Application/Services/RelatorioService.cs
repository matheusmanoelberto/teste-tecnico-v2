using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialControl.Application.DTOs;
using FinancialControl.Domain.Enums;
using FinancialControl.Domain.Repositories;

namespace FinancialControl.Application.Services;

public class RelatorioService : IRelatorioService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public RelatorioService(
        ITransacaoRepository transacaoRepository,
        IPessoaRepository pessoaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<IEnumerable<RelatorioPessoaDTO>> GetTotaisPorPessoaAsync()
    {
        var pessoas = await _pessoaRepository.GetAllAsync();
        var transacoes = await _transacaoRepository.GetAllAsync();

        var relatorio = pessoas.Select(p => 
        {
            var transacoesPessoa = transacoes.Where(t => t.PessoaId == p.Id).ToList();
            var receitas = transacoesPessoa.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor.Quantia);
            var despesas = transacoesPessoa.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor.Quantia);
            
            return new RelatorioPessoaDTO
            {
                NomePessoa = p.Nome,
                TotalReceitas = receitas,
                TotalDespesas = despesas,
                Saldo = receitas - despesas
            };
        });

        return relatorio;
    }

    public async Task<object> GetTotalGeralAsync()
    {
        var transacoes = await _transacaoRepository.GetAllAsync();
        var receitas = transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor.Quantia);
        var despesas = transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor.Quantia);

        return new
        {
            TotalGeraLReceitas = receitas,
            TotalGeralDespesas = despesas,
            SaldoLiquido = receitas - despesas
        };
    }
}
