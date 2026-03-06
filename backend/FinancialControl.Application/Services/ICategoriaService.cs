using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialControl.Application.DTOs;

namespace FinancialControl.Application.Services;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDTO>> GetAllAsync();
    Task<CategoriaDTO> AddAsync(CategoriaDTO categoriaDto);
    Task DeleteAsync(int id);
}
