using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialControl.Application.DTOs;

namespace FinancialControl.Application.Services;

public interface IPessoaService
{
    Task<IEnumerable<PessoaDTO>> GetAllAsync();
    Task<PessoaDTO> GetByIdAsync(int id);
    Task<PessoaDTO> AddAsync(PessoaDTO pessoaDto);
    Task UpdateAsync(int id, PessoaDTO pessoaDto);
    Task DeleteAsync(int id);
}
