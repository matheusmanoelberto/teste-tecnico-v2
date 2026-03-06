using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialControl.Application.DTOs;

namespace FinancialControl.Application.Services;

public interface IRelatorioService
{
    Task<IEnumerable<RelatorioPessoaDTO>> GetTotaisPorPessoaAsync();
    Task<object> GetTotalGeralAsync();
}
