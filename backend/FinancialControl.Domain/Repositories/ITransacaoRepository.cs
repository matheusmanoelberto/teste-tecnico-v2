using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialControl.Domain.Entities;

namespace FinancialControl.Domain.Repositories;

public interface ITransacaoRepository
{
    Task<Transacao> GetByIdAsync(int id);
    Task<IEnumerable<Transacao>> GetAllAsync();
    Task<Transacao> AddAsync(Transacao transacao);
    Task DeleteAsync(int id);
}
