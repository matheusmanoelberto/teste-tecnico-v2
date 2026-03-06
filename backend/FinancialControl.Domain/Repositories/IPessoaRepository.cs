using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialControl.Domain.Entities;

namespace FinancialControl.Domain.Repositories;

public interface IPessoaRepository
{
    Task<Pessoa> GetByIdAsync(int id);
    Task<IEnumerable<Pessoa>> GetAllAsync();
    Task<Pessoa> AddAsync(Pessoa pessoa);
    void Update(Pessoa pessoa);
    void Delete(Pessoa pessoa);
    Task SaveChangesAsync();
}
