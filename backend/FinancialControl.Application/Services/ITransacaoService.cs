using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialControl.Application.DTOs;

namespace FinancialControl.Application.Services;

public interface ITransacaoService
{
    Task<IEnumerable<TransacaoDTO>> GetAllAsync();
    Task<TransacaoDTO> AddAsync(TransacaoDTO transacaoDto);
    Task DeleteAsync(int id);
}
