using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialControl.Application.DTOs;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Exceptions;
using FinancialControl.Domain.Repositories;

namespace FinancialControl.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;

    public CategoriaService(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
    {
        var categorias = await _repository.GetAllAsync();
        return categorias.Select(c => new CategoriaDTO
        {
            Id = c.Id,
            Descricao = c.Descricao,
            Finalidade = c.Finalidade
        });
    }

    public async Task<CategoriaDTO> AddAsync(CategoriaDTO categoriaDto)
    {
        var categoria = new Categoria(categoriaDto.Descricao, categoriaDto.Finalidade);
        var created = await _repository.AddAsync(categoria);

        categoriaDto.Id = created.Id;
        return categoriaDto;
    }

    public async Task DeleteAsync(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);
        DomainException.When(categoria == null, "Categoria não encontrada.");
        await _repository.DeleteAsync(id);
    }
}
