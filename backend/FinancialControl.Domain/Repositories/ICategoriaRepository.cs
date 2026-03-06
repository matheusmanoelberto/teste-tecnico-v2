using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialControl.Domain.Entities;

namespace FinancialControl.Domain.Repositories;

public interface ICategoriaRepository
{
    Task<Categoria> GetByIdAsync(int id);
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria> AddAsync(Categoria categoria);
    Task DeleteAsync(int id);
}
